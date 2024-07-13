using Domain.Core.BaseType;
using System.Text.RegularExpressions;

namespace Domain.Members.ValueObjects;

public sealed class Email : ValueObject
{
    public const int MaxLength = 50;

    private Email(string value) => Value = value;

    public string Value { get; }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException();
        }

        if (value.Length > MaxLength)
        {
            throw new ArgumentException();
        }

        if (!IsValidEmail(value))
        {
            throw new ArgumentException();
        }

        return new Email(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValidEmail(string email)
    {
        // Regular expression pattern for basic email validation
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }
}
