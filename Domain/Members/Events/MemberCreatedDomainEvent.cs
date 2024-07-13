using Domain.Core.Events;

namespace Domain.Members.Events;

public sealed record MemberCreatedDomainEvent(Member Member) : DomainEvent;
