using ModelContextProtocol.Protocol.Transport;
using ModelContextProtocol.Utils;
using Microsoft.Extensions.Logging;

namespace ModelContextProtocol.Server;

/// <summary>
/// Provides a factory for creating <see cref="IMcpServer"/> instances.
/// </summary>
public static class McpServerFactory
{
    /// <summary>
    /// Initializes a new instance of the <see cref="McpServerFactory"/> class.
    /// </summary>
    /// <param name="serverTransport">Transport to use for the server</param>
    /// <param name="serverOptions">
    /// Configuration options for this server, including capabilities. 
    /// Make sure to accurately reflect exactly what capabilities the server supports and does not support.
    /// </param>
    /// <param name="serviceProvider">Optional service provider to create new instances.</param>
    /// <param name="loggerFactory">Logger factory to use for logging</param>
    /// <returns>An <see cref="McpServer"/> that's started and ready to receive connections.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="serverTransport"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="serverOptions"/> is <see langword="null"/>.</exception>
    public static IMcpServer Create(
        ITransport serverTransport,
        McpServerOptions serverOptions,
        ILoggerFactory? loggerFactory = null,
        IServiceProvider? serviceProvider = null)
    {
        Throw.IfNull(serverTransport);
        Throw.IfNull(serverOptions);

        return new McpServer(serverTransport, serverOptions, loggerFactory, serviceProvider);
    }
}
