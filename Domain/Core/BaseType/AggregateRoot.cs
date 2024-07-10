﻿using Domain.Core.Events;

namespace Domain.Core.BaseType;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(Guid id) : base(id) { }

    protected AggregateRoot() { }

    private readonly List<IDomainEvent> domainEvents = new List<IDomainEvent>();

    public IReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();

    public void RaiseDomainEvent(IDomainEvent @event) => domainEvents.Add(@event);
}
