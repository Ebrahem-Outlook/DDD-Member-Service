using Domain.Core.Events;
using Domain.Members.ValueObjects;

namespace Domain.Members.Events;

public sealed record MemberEmailUpdatedDomainEvent(
    Guid MemerId,
    Email Email) : IDomainEvent;
