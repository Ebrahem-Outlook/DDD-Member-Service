namespace Application.Core.Abstractions.Messaging;

public interface IIntegrationEventPublisher
{
    void Publish(IIntegrationEvent integrationEvent);
}
