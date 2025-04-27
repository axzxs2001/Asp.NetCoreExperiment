﻿namespace ModelContextProtocol.Protocol.Types;

/// <summary>
/// Sent from the client to request updated notifications from the server whenever a particular primitive changes.
/// <see href="https://github.com/modelcontextprotocol/specification/blob/main/schema/2024-11-05/schema.json">See the schema for details</see>
/// </summary>
public class SubscribeRequestParams : RequestParams
{
    /// <summary>
    /// The URI of the resource to subscribe to. The URI can use any protocol; it is up to the server how to interpret it.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("uri")]
    public string? Uri { get; init; }
}
