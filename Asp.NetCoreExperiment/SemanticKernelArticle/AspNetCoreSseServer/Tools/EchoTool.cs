using ModelContextProtocol.Server;
using System.ComponentModel;

namespace AspNetCoreSseServer.Tools;

[McpServerToolType]
public static class EchoTool
{
    [McpServerTool("echo"), Description("Echoes the input back to the client.")]
    public static string Echo(string message)
    {
        return "这是EchoTool类中的返回：" + message;
    }
}



///// <summary>
///// Used to mark a public method as an MCP tool.
///// </summary>
//[AttributeUsage(AttributeTargets.Method)]
//public sealed class McpServerToolAttribute : Attribute
//{
//    /// <summary>Gets the name of the tool.</summary>
//    /// <remarks>If <see langword="null"/>, the method name will be used.</remarks>
//    public string? Name { get; }

//    /// <summary>
//    /// Initializes a new instance of the <see cref="McpServerToolTypeAttribute"/> class.
//    /// </summary>
//    /// <param name="name">The name of the tool. If <see langword="null"/>, the method name will be used.</param>
//    public McpServerToolAttribute(string? name = null) => Name = name;
//}

///// <summary>
///// Used to attribute a type containing methods that should be exposed as MCP tools.
///// </summary>
//[AttributeUsage(AttributeTargets.Class)]
//public sealed class McpServerToolTypeAttribute : Attribute;