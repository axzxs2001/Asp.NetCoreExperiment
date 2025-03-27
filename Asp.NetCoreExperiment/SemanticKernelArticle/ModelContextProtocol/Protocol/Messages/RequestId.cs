using System.Text.Json.Serialization;

namespace ModelContextProtocol.Protocol.Messages;

/// <summary>
/// Represents a JSON-RPC request identifier which can be either a string or a number.
/// </summary>
[JsonConverter(typeof(RequestIdConverter))]
public readonly struct RequestId : IEquatable<RequestId>
{
    private readonly object _value;

    private RequestId(object value)
    {
        _value = value;
    }

    /// <summary>
    /// Creates a new RequestId from a string.
    /// </summary>
    /// <param name="value">The Id</param>
    /// <returns>Wrapped Id object</returns>
    public static RequestId FromString(string value) => new(value);

    /// <summary>
    /// Creates a new RequestId from a number.
    /// </summary>
    /// <param name="value">The Id</param>
    /// <returns>Wrapped Id object</returns>
    public static RequestId FromNumber(long value) => new(value);

    /// <summary>
    /// Checks if the RequestId is a string.
    /// </summary>
    public bool IsString => _value is string;

    /// <summary>
    /// Checks if the RequestId is a number.
    /// </summary>
    public bool IsNumber => _value is long;

    /// <summary>
    /// Checks if the request id is valid (has a value)
    /// </summary>
    public bool IsValid => _value != null;

    /// <summary>
    /// Gets the RequestId as a string.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the RequestId is not a string</exception>"
    public string AsString => _value as string ?? throw new InvalidOperationException("RequestId is not a string");

    /// <summary>
    /// Gets the RequestId as a number.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the RequestId is not a number</exception>""
    public long AsNumber => _value is long number ? number : throw new InvalidOperationException("RequestId is not a number");

    /// <summary>
    /// Returns the string representation of the RequestId. Will box the value if it is a number.
    /// </summary>
    public override string ToString() => _value.ToString() ?? "";

    /// <summary>
    /// Compares this RequestId to another RequestId.
    /// </summary>
    public bool Equals(RequestId other) => _value.Equals(other._value);

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is RequestId other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() => _value.GetHashCode();

    /// <summary>
    /// Compares two RequestIds for equality.
    /// </summary>
    public static bool operator ==(RequestId left, RequestId right) => left.Equals(right);

    /// <summary>
    /// Compares two RequestIds for inequality.
    /// </summary>
    public static bool operator !=(RequestId left, RequestId right) => !left.Equals(right);
}
