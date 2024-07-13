using Domain.Core.BaseType;

namespace Domain.Members.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 50;

    private FirstName(string value) => Value = value;

    public string Value { get; }

    public static FirstName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException();
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException();
        }

        return new FirstName(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
