﻿using System.Reflection;
using ModelContextProtocol.Server;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Utils;

namespace ModelContextProtocol.Configuration;

/// <summary>
/// Configures the McpServerOptions using addition services from DI.
/// </summary>
/// <param name="serverHandlers">The server handlers configuration options.</param>
/// <param name="serverTools">Tools individually registered.</param>
internal sealed class McpServerOptionsSetup(
    IOptions<McpServerHandlers> serverHandlers,
    IEnumerable<McpServerTool> serverTools) : IConfigureOptions<McpServerOptions>
{
    /// <summary>
    /// Configures the given McpServerOptions instance by setting server information
    /// and applying custom server handlers and tools.
    /// </summary>
    /// <param name="options">The options instance to be configured.</param>
    public void Configure(McpServerOptions options)
    {
        Throw.IfNull(options);

        // Configure the option's server information based on the current process,
        // if it otherwise lacks server information.
        if (options.ServerInfo is not { } serverInfo)
        {
            var assemblyName = (Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).GetName();
            options.ServerInfo = new()
            {
                Name = assemblyName.Name ?? "McpServer",
                Version = assemblyName.Version?.ToString() ?? "1.0.0",
            };
        }

        // Collect all of the provided tools into a tools collection. If the options already has
        // a collection, add to it, otherwise create a new one. We want to maintain the identity
        // of an existing collection in case someone has provided their own derived type, wants
        // change notifications, etc.
        McpServerToolCollection toolsCollection = options.Capabilities?.Tools?.ToolCollection ?? [];
        foreach (var tool in serverTools)
        {
            toolsCollection.TryAdd(tool);
        }

        if (!toolsCollection.IsEmpty)
        {
            options.Capabilities ??= new();
            options.Capabilities.Tools ??= new();
            options.Capabilities.Tools.ToolCollection = toolsCollection;
        }

        // Apply custom server handlers.
        serverHandlers.Value.OverwriteWithSetHandlers(options);
    }
}
