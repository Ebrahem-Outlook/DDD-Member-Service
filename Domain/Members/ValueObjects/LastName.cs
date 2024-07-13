using Domain.Core.BaseType;

namespace Domain.Members.ValueObjects;

public sealed class LastName : ValueObject
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

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
