namespace ModelContextProtocol.Protocol.Transport;

/// <summary>
/// Options for configuring the SSE transport.
/// </summary>
public record SseClientTransportOptions
{
    /// <summary>
    /// Timeout for initial connection and endpoint event.
    /// </summary>
    public TimeSpan ConnectionTimeout { get; init; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Number of reconnection attempts for SSE connection.
    /// </summary>
    public int MaxReconnectAttempts { get; init; } = 3;

    /// <summary>
    /// Delay between reconnection attempts.
    /// </summary>
    public TimeSpan ReconnectDelay { get; init; } = TimeSpan.FromSeconds(5);

    /// <summary>
    /// Headers to include in HTTP requests.
    /// </summary>
    public Dictionary<string, string>? AdditionalHeaders { get; init; }
}