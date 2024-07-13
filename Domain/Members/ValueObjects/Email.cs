using Domain.Core.BaseType;

namespace Domain.Members.ValueObjects;

public sealed class Email : ValueObject
{
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}
