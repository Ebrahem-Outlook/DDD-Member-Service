using Domain.Core.BaseType;

namespace Domain.Members.ValueObjects;

public sealed class Password : ValueObject
{
    public const int MinLength = 6;
    public const int MaxLength = 50;

    private Password(string value) => Value = value;

    public string Value { get; }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException();
        }

        if (value.Length > MaxLength || value.Length < MinLength)
        {
            throw new ArgumentException();
        }

        return new Password(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
