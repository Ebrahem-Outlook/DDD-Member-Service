using Domain.Core.Events;
using Domain.Members.ValueObjects;

namespace Domain.Members.Events;

public sealed record MemberPasswordUpdatedDomainEvent(
    Guid MemberId,
    Password Password) : IDomainEvent;
