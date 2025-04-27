namespace ModelContextProtocol.Protocol.Transport;

/// <summary>
/// Represents a transport mechanism for MCP communication (from the server).
/// </summary>
public interface IServerTransport : ITransport
{
    /// <summary>
    /// Starts listening for incoming messages.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task StartListeningAsync(CancellationToken cancellationToken = default);
}
