namespace IP.Shared.IntegrationEvents.Contracts;

public interface IIntegrationEvent;

public interface IIntegrationEventHandler<in T> where T : IIntegrationEvent
{
    Task Handle(T domainEvent, CancellationToken cancellationToken);
}