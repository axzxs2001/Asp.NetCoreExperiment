using System.Threading.Channels;
using ModelContextProtocol.Protocol.Messages;

namespace ModelContextProtocol.Protocol.Transport;

/// <summary>
/// Represents a transport mechanism for MCP communication.
/// </summary>
public interface ITransport : IAsyncDisposable
{
    /// <summary>
    /// Gets whether the transport is currently connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Channel for receiving messages from the transport.
    /// </summary>
    ChannelReader<IJsonRpcMessage> MessageReader { get; }

    /// <summary>
    /// Sends a message through the transport.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task SendMessageAsync(IJsonRpcMessage message, CancellationToken cancellationToken = default);
}
