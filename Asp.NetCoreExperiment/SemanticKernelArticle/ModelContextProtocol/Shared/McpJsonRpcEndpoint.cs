﻿using ModelContextProtocol.Client;
using ModelContextProtocol.Logging;
using ModelContextProtocol.Protocol.Messages;
using ModelContextProtocol.Protocol.Transport;
using ModelContextProtocol.Utils;
using ModelContextProtocol.Utils.Json;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using System.Collections.Concurrent;
using System.Text.Json;

namespace ModelContextProtocol.Shared;

/// <summary>
/// Base class for an MCP JSON-RPC endpoint. This covers both MCP clients and servers.
/// It is not supported, nor necessary, to implement both client and server functionality in the same class.
/// If an application needs to act as both a client and a server, it should use separate objects for each.
/// This is especially true as a client represents a connection to one and only one server, and vice versa.
/// Any multi-client or multi-server functionality should be implemented at a higher level of abstraction.
/// </summary>
internal abstract class McpJsonRpcEndpoint : IAsyncDisposable
{
    private readonly ITransport _transport;
    private readonly ConcurrentDictionary<RequestId, TaskCompletionSource<IJsonRpcMessage>> _pendingRequests;
    private readonly ConcurrentDictionary<string, List<Func<JsonRpcNotification, Task>>> _notificationHandlers;
    private readonly Dictionary<string, Func<JsonRpcRequest, CancellationToken, Task<object?>>> _requestHandlers = [];
    private int _nextRequestId;
    private readonly JsonSerializerOptions _jsonOptions;
    private bool _isDisposed;

    protected readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="McpJsonRpcEndpoint"/> class.
    /// </summary>
    /// <param name="transport">An MCP transport implementation.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    protected McpJsonRpcEndpoint(ITransport transport, ILoggerFactory? loggerFactory = null)
    {
        Throw.IfNull(transport);

        _transport = transport;
        _pendingRequests = new();
        _notificationHandlers = new();
        _nextRequestId = 1;
        _jsonOptions = McpJsonUtilities.DefaultOptions;
        _logger = loggerFactory?.CreateLogger(GetType()) ?? NullLogger.Instance;
    }

    /// <summary>
    /// Gets whether the endpoint is initialized and ready to process messages.
    /// </summary>
    public bool IsInitialized { get; set; }

    /// <summary>
    /// Gets the name of the endpoint for logging and debug purposes.
    /// </summary>
    public abstract string EndpointName { get; }

    /// <summary>
    /// Gets the transport implementation for the endpoint. Should generally not be needed outside of tests.
    /// Sub-classes should store IClientTransport or IServerTransport injected during construction instead of casting this field.
    /// </summary>
    internal ITransport Transport => _transport;

    /// <summary>
    /// Starts processing messages from the transport. This method will block until the transport is disconnected.
    /// This is generally started in a background task or thread from the initialization logic of the derived class.
    /// </summary>
    internal async Task ProcessMessagesAsync(CancellationToken cancellationToken)
    {
        try
        {
            await foreach (var message in _transport.MessageReader.ReadAllAsync(cancellationToken).ConfigureAwait(false))
            {
                _logger.TransportMessageRead(EndpointName, message.GetType().Name);

                // Fire and forget the message handling task to avoid blocking the transport
                // If awaiting the task, the transport will not be able to read more messages,
                // which could lead to a deadlock if the handler sends a message back
                _ = ProcessMessageAsync();
                async Task ProcessMessageAsync()
                {
#if NET
                    await Task.CompletedTask.ConfigureAwait(ConfigureAwaitOptions.ForceYielding);
#else
                    await default(ForceYielding);
#endif
                    try
                    {
                        await HandleMessageAsync(message, cancellationToken).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        var payload = JsonSerializer.Serialize(message, _jsonOptions.GetTypeInfo<IJsonRpcMessage>());
                        _logger.MessageHandlerError(EndpointName, message.GetType().Name, payload, ex);
                    }
                }
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            // Normal shutdown
            _logger.EndpointMessageProcessingCancelled(EndpointName);
        }
        catch (NullReferenceException)
        {
            // Ignore reader disposal and mocked transport
        }
    }

    private async Task HandleMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken)
    {
        switch (message)
        {
            case JsonRpcRequest request:
                await HandleRequest(request, cancellationToken).ConfigureAwait(false);
                break;

            case IJsonRpcMessageWithId messageWithId:
                HandleMessageWithId(message, messageWithId);
                break;

            case JsonRpcNotification notification:
                await HandleNotification(notification).ConfigureAwait(false);
                break;

            default:
                _logger.EndpointHandlerUnexpectedMessageType(EndpointName, message.GetType().Name);
                break;
        }
    }

    private async Task HandleNotification(JsonRpcNotification notification)
    {
        if (_notificationHandlers.TryGetValue(notification.Method, out var handlers))
        {
            foreach (var notificationHandler in handlers)
            {
                try
                {
                    await notificationHandler(notification).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    // Log handler error but continue processing
                    _logger.NotificationHandlerError(EndpointName, notification.Method, ex);
                }
            }
        }
    }

    private void HandleMessageWithId(IJsonRpcMessage message, IJsonRpcMessageWithId messageWithId)
    {
        if (!messageWithId.Id.IsValid)
        {
            _logger.RequestHasInvalidId(EndpointName);
        }
        else if (_pendingRequests.TryRemove(messageWithId.Id, out var tcs))
        {
            _logger.ResponseMatchedPendingRequest(EndpointName, messageWithId.Id.ToString());
            tcs.TrySetResult(message);
        }
        else
        {
            _logger.NoRequestFoundForMessageWithId(EndpointName, messageWithId.Id.ToString());
        }
    }

    private async Task HandleRequest(JsonRpcRequest request, CancellationToken cancellationToken)
    {
        if (_requestHandlers.TryGetValue(request.Method, out var handler))
        {
            try
            {
                _logger.RequestHandlerCalled(EndpointName, request.Method);
                var result = await handler(request, cancellationToken).ConfigureAwait(false);
                _logger.RequestHandlerCompleted(EndpointName, request.Method);
                await _transport.SendMessageAsync(new JsonRpcResponse
                {
                    Id = request.Id,
                    JsonRpc = "2.0",
                    Result = result
                }, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.RequestHandlerError(EndpointName, request.Method, ex);
                // Send error response
                await _transport.SendMessageAsync(new JsonRpcError
                {
                    Id = request.Id,
                    JsonRpc = "2.0",
                    Error = new JsonRpcErrorDetail
                    {
                        Code = -32000,  // Implementation defined error
                        Message = ex.Message
                    }
                }, cancellationToken).ConfigureAwait(false);
            }
        }
        else
        {
            _logger.NoHandlerFoundForRequest(EndpointName, request.Method);
        }
    }

    /// <summary>
    /// Sends a generic JSON-RPC request to the server.
    /// It is strongly recommended use the capability-specific methods instead of this one.
    /// Use this method for custom requests or those not yet covered explicitly by the endpoint implementation.
    /// </summary>
    /// <typeparam name="TResult">The expected response type.</typeparam>
    /// <param name="request">The JSON-RPC request to send.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task containing the server's response.</returns>
    public async Task<TResult> SendRequestAsync<TResult>(JsonRpcRequest request, CancellationToken cancellationToken) where TResult : class
    {
        if (!_transport.IsConnected)
        {
            _logger.EndpointNotConnected(EndpointName);
            throw new McpClientException("Transport is not connected");
        }

        // Set request ID
        request.Id = RequestId.FromNumber(Interlocked.Increment(ref _nextRequestId));

        var tcs = new TaskCompletionSource<IJsonRpcMessage>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pendingRequests[request.Id] = tcs;

        try
        {
            // Expensive logging, use the logging framework to check if the logger is enabled
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.SendingRequestPayload(EndpointName, JsonSerializer.Serialize(request, _jsonOptions.GetTypeInfo<JsonRpcRequest>()));
            }

            // Less expensive information logging
            _logger.SendingRequest(EndpointName, request.Method);

            await _transport.SendMessageAsync(request, cancellationToken).ConfigureAwait(false);

            _logger.RequestSentAwaitingResponse(EndpointName, request.Method, request.Id.ToString());
            var response = await tcs.Task.WaitAsync(cancellationToken).ConfigureAwait(false);

            if (response is JsonRpcError error)
            {
                _logger.RequestFailed(EndpointName, request.Method, error.Error.Message, error.Error.Code);
                throw new McpClientException($"Request failed (server side): {error.Error.Message}", error.Error.Code);
            }

            if (response is JsonRpcResponse success)
            {
                // Convert the Result object to JSON and back to get our strongly-typed result
                var resultJson = JsonSerializer.Serialize(success.Result, _jsonOptions.GetTypeInfo<object?>());
                var resultObject = JsonSerializer.Deserialize(resultJson, _jsonOptions.GetTypeInfo<TResult>());

                // Not expensive logging because we're already converting to JSON in order to get the result object
                _logger.RequestResponseReceivedPayload(EndpointName, resultJson);
                _logger.RequestResponseReceived(EndpointName, request.Method);

                if (resultObject != null)
                {
                    return resultObject;
                }

                // Result object was null, this is unexpected
                _logger.RequestResponseTypeConversionError(EndpointName, request.Method, typeof(TResult));
                throw new McpClientException($"Unexpected response type {JsonSerializer.Serialize(success.Result, _jsonOptions.GetTypeInfo<TResult>())}, expected {typeof(TResult)}");
            }

            // Unexpected response type
            _logger.RequestInvalidResponseType(EndpointName, request.Method);
            throw new McpClientException("Invalid response type");
        }
        finally
        {
            _pendingRequests.TryRemove(request.Id, out _);
        }
    }

    public Task SendMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken = default)
    {
        Throw.IfNull(message);

        if (!_transport.IsConnected)
        {
            _logger.ClientNotConnected(EndpointName);
            throw new McpClientException("Transport is not connected");
        }

        if (_logger.IsEnabled(LogLevel.Debug))
        {
            _logger.SendingMessage(EndpointName, JsonSerializer.Serialize(message, _jsonOptions.GetTypeInfo<IJsonRpcMessage>()));
        }

        return _transport.SendMessageAsync(message, cancellationToken);
    }

    /// <summary>
    /// Registers a handler for incoming notifications of a specific method.
    /// 
    /// <see cref="NotificationMethods">Constants for common notification methods</see>
    /// </summary>
    /// <param name="method">The notification method to handle.</param>
    /// <param name="handler">The async handler function to process notifications.</param>
    public void AddNotificationHandler(string method, Func<JsonRpcNotification, Task> handler)
    {
        var handlers = _notificationHandlers.GetOrAdd(method, _ => []);
        lock (handlers)
        {
            handlers.Add(handler);
        }
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await CleanupAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Registers a handler for incoming requests of a specific method.
    /// </summary>
    /// <typeparam name="TRequest">Type of request payload</typeparam>
    /// <typeparam name="TResponse">Type of response payload (not full RPC response</typeparam>
    /// <param name="method">Method identifier to register for</param>
    /// <param name="handler">Handler to be called when a request with specified method identifier is received</param>
    protected void SetRequestHandler<TRequest, TResponse>(string method, Func<TRequest?, CancellationToken, Task<TResponse>> handler)
    {
        Throw.IfNull(method);
        Throw.IfNull(handler);

        _requestHandlers[method] = async (request, cancellationToken) =>
        {
            // Convert the params JsonElement to our type using the same options
            var jsonString = JsonSerializer.Serialize(request.Params, _jsonOptions.GetTypeInfo<object?>());
            var typedRequest = JsonSerializer.Deserialize(jsonString, _jsonOptions.GetTypeInfo<TRequest>());

            return await handler(typedRequest, cancellationToken).ConfigureAwait(false);
        };
    }

    /// <summary>
    /// Task that processes incoming messages from the transport.
    /// </summary>
    protected Task? MessageProcessingTask { get; set; }

    /// <summary>
    /// CancellationTokenSource used to cancel the message processing task.
    /// </summary>
    protected CancellationTokenSource? CancellationTokenSource { get; set; }

    /// <summary>
    /// Cleans up the endpoint and releases resources.
    /// </summary>
    /// <returns></returns>
    protected async Task CleanupAsync()
    {
        if (_isDisposed)
            return;

        _isDisposed = true;

        _logger.CleaningUpEndpoint(EndpointName);

        if (CancellationTokenSource != null)
            await CancellationTokenSource.CancelAsync().ConfigureAwait(false);

        if (MessageProcessingTask != null)
        {
            try
            {
                await MessageProcessingTask.ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                // Ignore cancellation
            }
        }

        // Complete all pending requests with cancellation
        foreach (var entry in _pendingRequests)
        {
            entry.Value.TrySetCanceled();
        }
        _pendingRequests.Clear();

        await _transport.DisposeAsync().ConfigureAwait(false);
        CancellationTokenSource?.Dispose();

        IsInitialized = false;

        _logger.EndpointCleanedUp(EndpointName);
    }
}
