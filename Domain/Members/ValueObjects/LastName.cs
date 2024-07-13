
namespace Domain.Members.ValueObjects;

public sealed class LastName : IEquatable<LastName?>
{
    public const int MaxLength = 50;

    private LastName(string value) => Value = value;

    public string Value { get; }

    public static LastName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException();
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException();
        }

        return new LastName(value);
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LastName);
    }

    public bool Equals(LastName? other)
    {
        return other is not null &&
               Value == other.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public static bool operator ==(LastName? left, LastName? right)
    {
        return EqualityComparer<LastName>.Default.Equals(left, right);
    }

    public static bool operator !=(LastName? left, LastName? right)
    {
        return !(left == right);
    }
}
