﻿using ModelContextProtocol.Protocol.Messages;
using ModelContextProtocol.Protocol.Types;

namespace ModelContextProtocol.Client;

/// <summary>
/// Represents an instance of an MCP client connecting to a specific server.
/// </summary>
public interface IMcpClient : IAsyncDisposable
{
    /// <summary>
    /// Gets the capabilities supported by the server.
    /// </summary>
    ServerCapabilities? ServerCapabilities { get; }

    /// <summary>
    /// Gets the version and implementation information of the server.
    /// </summary>
    Implementation? ServerInfo { get; }

    /// <summary>
    /// Instructions describing how to use the server and its features.
    /// This can be used by clients to improve the LLM's understanding of available tools, resources, etc. 
    /// It can be thought of like a "hint" to the model. For example, this information MAY be added to the system prompt.
    /// </summary>
    string? ServerInstructions { get; }

    /// <summary>
    /// Adds a handler for server notifications of a specific method.
    /// </summary>
    /// <param name="method">The notification method to handle.</param>
    /// <param name="handler">The async handler function to process notifications.</param>
    /// <remarks>
    /// <para>
    /// Each method may have multiple handlers. Adding a handler for a method that already has one
    /// will not replace the existing handler.
    /// </para>
    /// <para>
    /// <see cref="NotificationMethods"> provides constants for common notification methods.</see>
    /// </para>
    /// </remarks>
    void AddNotificationHandler(string method, Func<JsonRpcNotification, Task> handler);

    /// <summary>
    /// Sends a generic JSON-RPC request to the server.
    /// </summary>
    /// <typeparam name="TResult">The expected response type.</typeparam>
    /// <param name="request">The JSON-RPC request to send.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A task containing the server's response.</returns>
    /// <remarks>
    /// It is recommended to use the capability-specific methods that use this one in their implementation.
    /// Use this method for custom requests or those not yet covered explicitly.
    /// </remarks>
    Task<TResult> SendRequestAsync<TResult>(JsonRpcRequest request, CancellationToken cancellationToken = default) where TResult : class;

    /// <summary>
    /// Sends a message to the server.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task SendMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken = default);
}