using Domain.Core.Events;

namespace Domain.Members.Events;

public sealed record MemberPasswordUpdatedDomainEvent(
    Guid MemberId,
    string Password) : IDomainEvent;
