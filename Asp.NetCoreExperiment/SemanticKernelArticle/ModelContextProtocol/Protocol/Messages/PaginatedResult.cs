namespace ModelContextProtocol.Protocol.Messages;

/// <summary>
/// A paginated result payload.
/// </summary>
public class PaginatedResult
{
    /// <summary>
    /// An opaque token representing the pagination position after the last returned result.\nIf present, there may be more results available.
    /// </summary>
    public string? NextCursor { get; set; }
}