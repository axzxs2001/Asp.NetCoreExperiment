﻿namespace ModelContextProtocol.Protocol.Types;

/// <summary>
/// A request from the server to get a list of root URIs from the client.
/// <see href="https://github.com/modelcontextprotocol/specification/blob/main/schema/2024-11-05/schema.json">See the schema for details</see>
/// </summary>
public class ListRootsRequestParams
{
    /// <summary>
    /// Optional progress token for out-of-band progress notifications.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("progressToken")]
    public string? ProgressToken { get; init; }
}
