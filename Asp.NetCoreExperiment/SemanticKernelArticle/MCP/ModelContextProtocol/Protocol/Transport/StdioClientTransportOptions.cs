// Protocol/Transport/StdioTransport.cs
namespace ModelContextProtocol.Protocol.Transport;

/// <summary>
/// Represents configuration options for the stdio transport.
/// </summary>
public record StdioClientTransportOptions
{
    /// <summary>
    /// The default timeout to wait for the server to shut down gracefully.
    /// </summary>
    public static readonly TimeSpan DefaultShutdownTimeout = TimeSpan.FromSeconds(5);

    /// <summary>
    /// The command to execute to start the server process.
    /// </summary>
    public required string Command { get; set; }

    /// <summary>
    /// Arguments to pass to the server process.
    /// </summary>
    public string? Arguments { get; set; }

    /// <summary>
    /// The working directory for the server process.
    /// </summary>
    public string? WorkingDirectory { get; set; }

    /// <summary>
    /// Environment variables to set for the server process.
    /// </summary>
    public Dictionary<string, string>? EnvironmentVariables { get; set; }

    /// <summary>
    /// The timeout to wait for the server to shut down gracefully.
    /// </summary>
    public TimeSpan ShutdownTimeout { get; init; } = DefaultShutdownTimeout;
}
