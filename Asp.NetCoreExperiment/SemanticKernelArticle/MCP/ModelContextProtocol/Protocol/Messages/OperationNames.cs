namespace ModelContextProtocol.Protocol.Messages;

/// <summary>Provides names of standard operations for use with registering handlers.</summary>
/// <remarks>
/// These values should not be inspected or relied on for their exact values.
/// They serve only as opaque keys. They will be stable for the lifetime of a process
/// but may change between versions of this library.
/// </remarks>
public static class OperationNames
{
    /// <summary>Gets the name of the sampling operation.</summary>
    public static string Sampling { get; } = "operation/sampling";

    /// <summary>Gets the name of the roots operation.</summary>
    public static string Roots { get; } = "operation/roots";

    /// <summary>Gets the name of the list tools operation.</summary>
    public static string ListTools { get; } = "operation/listTools";

    /// <summary>Gets the name of the call tool operation.</summary>
    public static string CallTool { get; } = "operation/callTool";

    /// <summary>Gets the name of the list prompts operation.</summary>
    public static string ListPrompts { get; } = "operation/listPrompts";

    /// <summary>Gets the name of the get prompt operation.</summary>
    public static string GetPrompt { get; } = "operation/getPrompt";

    /// <summary>Gets the name of the list resources operation.</summary>
    public static string ListResources { get; } = "operation/listResources";

    /// <summary>Gets the name of the read resource operation.</summary>
    public static string ReadResource { get; } = "operation/readResource";

    /// <summary>Gets the name of the get completion operation.</summary>
    public static string GetCompletion { get; } = "operation/getCompletion";

    /// <summary>Gets the name of the subscribe to resources operation.</summary>
    public static string SubscribeToResources { get; } = "operation/subscribeToResources";

    /// <summary>Gets the name of the subscribe to resources operation.</summary>
    public static string UnsubscribeFromResources { get; } = "operation/unsubscribeFromResources";
}