using Domain.Core.Events;
using Domain.Members.ValueObjects;

namespace Domain.Members.Events;

public sealed record MemberNameUpdatedDomainEvent(
    Guid MemberId, 
    FirstName FirstName, 
    LastName LastName) : IDomainEvent;
