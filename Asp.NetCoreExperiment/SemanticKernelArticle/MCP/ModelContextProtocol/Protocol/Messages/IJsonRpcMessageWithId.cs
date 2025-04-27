namespace ModelContextProtocol.Protocol.Messages;

/// <summary>
/// Base interface for JSON-RPC messages that include an ID.
/// </summary>
public interface IJsonRpcMessageWithId : IJsonRpcMessage
{
    /// <summary>
    /// The message identifier.
    /// </summary>
    RequestId Id { get; }
}
