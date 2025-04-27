﻿using ModelContextProtocol.Configuration;
using ModelContextProtocol.Server;
using ModelContextProtocol.Utils;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ModelContextProtocol;

/// <summary>
/// Extension to configure the MCP server with tools
/// </summary>
public static partial class McpServerBuilderExtensions
{
    private const string RequiresUnreferencedCodeMessage = "This method requires dynamic lookup of method metadata and might not work in Native AOT.";

    /// <summary>Adds <see cref="McpServerTool"/> instances to the service collection backing <paramref name="builder"/>.</summary>
    /// <typeparam name="TTool">The tool type.</typeparam>
    /// <param name="builder">The builder instance.</param>
    /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// This method discovers all instance and static methods (public and non-public) on the specified <typeparamref name="TTool"/>
    /// type, where the methods are attributed as <see cref="McpServerToolAttribute"/>, and adds an <see cref="McpServerTool"/>
    /// instance for each. For instance methods, an instance will be constructed for each invocation of the tool.
    /// </remarks>
    public static IMcpServerBuilder WithTools<[DynamicallyAccessedMembers(
        DynamicallyAccessedMemberTypes.PublicMethods | 
        DynamicallyAccessedMemberTypes.NonPublicMethods |
        DynamicallyAccessedMemberTypes.PublicConstructors)] TTool>(
        this IMcpServerBuilder builder)
    {
        Throw.IfNull(builder);

        foreach (var toolMethod in typeof(TTool).GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
        {
            if (toolMethod.GetCustomAttribute<McpServerToolAttribute>() is not null)
            {
                if (toolMethod.IsStatic)
                {
                    builder.Services.AddSingleton(services => McpServerTool.Create(toolMethod, services: services));
                }
                else
                {
                    builder.Services.AddSingleton(services => McpServerTool.Create(toolMethod, typeof(TTool), services: services));
                }
            }
        }

        return builder;
    }

    /// <summary>Adds <see cref="McpServerTool"/> instances to the service collection backing <paramref name="builder"/>.</summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="toolTypes">Types with marked methods to add as tools to the server.</param>
    /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="toolTypes"/> is <see langword="null"/>.</exception>
    /// <remarks>
    /// This method discovers all instance and static methods (public and non-public) on the specified <paramref name="toolTypes"/>
    /// types, where the methods are attributed as <see cref="McpServerToolAttribute"/>, and adds an <see cref="McpServerTool"/>
    /// instance for each. For instance methods, an instance will be constructed for each invocation of the tool.
    /// </remarks>
    [RequiresUnreferencedCode(RequiresUnreferencedCodeMessage)]
    public static IMcpServerBuilder WithTools(this IMcpServerBuilder builder, params IEnumerable<Type> toolTypes)
    {
        Throw.IfNull(builder);
        Throw.IfNull(toolTypes);

        foreach (var toolType in toolTypes)
        {
            if (toolType is not null)
            {
                foreach (var method in toolType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
                {
                    if (method.GetCustomAttribute<McpServerToolAttribute>() is not null)
                    {
                        if (method.IsStatic)
                        {
                            builder.Services.AddSingleton(services => McpServerTool.Create(method, services: services));
                        }
                        else
                        {
                            builder.Services.AddSingleton(services => McpServerTool.Create(method, toolType, services: services));
                        }
                    }
                }
            }
        }

        return builder;
    }

    /// <summary>
    /// Adds types marked with the <see cref="McpServerToolTypeAttribute"/> attribute from the given assembly as tools to the server.
    /// </summary>
    /// <param name="builder">The builder instance.</param>
    /// <param name="toolAssembly">The assembly to load the types from. Null to get the current assembly</param>
    /// <exception cref="ArgumentNullException"><paramref name="builder"/> is <see langword="null"/>.</exception>
    [RequiresUnreferencedCode(RequiresUnreferencedCodeMessage)]
    public static IMcpServerBuilder WithToolsFromAssembly(this IMcpServerBuilder builder, Assembly? toolAssembly = null)
    {
        Throw.IfNull(builder);

        toolAssembly ??= Assembly.GetCallingAssembly();

        return builder.WithTools(
            from t in toolAssembly.GetTypes()
            where t.GetCustomAttribute<McpServerToolTypeAttribute>() is not null
            select t);
    }
}
