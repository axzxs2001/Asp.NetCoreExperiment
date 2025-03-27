using ModelContextProtocol.Utils;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ModelContextProtocol.Protocol.Messages;

/// <summary>
/// JSON converter for RequestId that handles both string and number values.
/// </summary>
public class RequestIdConverter : JsonConverter<RequestId>
{
    /// <inheritdoc />
    public override RequestId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.String => RequestId.FromString(reader.GetString()!),
            JsonTokenType.Number => RequestId.FromNumber(reader.GetInt64()),
            _ => throw new JsonException("RequestId must be either a string or a number"),
        };
    }

    /// <inheritdoc />
    public override void Write(Utf8JsonWriter writer, RequestId value, JsonSerializerOptions options)
    {
        Throw.IfNull(writer);

        if (value.IsString)
        {
            writer.WriteStringValue(value.AsString);
        }
        else
        {
            writer.WriteNumberValue(value.AsNumber);
        }
    }
}