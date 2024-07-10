using Domain.Core.Events;

namespace Domain.Members.Events;

public sealed record MemberEmailUpdatedDomainEvent(
    Guid MemerId,
    string Email) : IDomainEvent;
