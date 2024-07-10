using Domain.Core.Events;

namespace Domain.Members.Events;

public sealed record MemberNameUpdatedDomainEvent(
    Guid MemberId, 
    string FirstName, 
    string LastName) : IDomainEvent;
