using Domain.Members;

namespace Application.Core.Abstractions.Authentications;

public interface IMemberIdentifierProvider
{
    Guid MemberId { get; }
}
