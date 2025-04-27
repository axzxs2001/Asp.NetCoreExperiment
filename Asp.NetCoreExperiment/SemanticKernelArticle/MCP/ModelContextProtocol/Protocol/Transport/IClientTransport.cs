namespace ModelContextProtocol.Protocol.Transport;

/// <summary>
/// Represents a transport mechanism for MCP communication (from the client).
/// </summary>
public interface IClientTransport : ITransport
{
    /// <summary>
    /// Establishes the transport connection.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    Task ConnectAsync(CancellationToken cancellationToken = default);
}
