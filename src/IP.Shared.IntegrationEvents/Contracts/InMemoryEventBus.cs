using System.Reflection;

namespace IP.Shared.IntegrationEvents.Contracts;

public interface IEventBus
{
    public Task Publish(IIntegrationEvent @event);
    Task PublishMany(IEnumerable<IIntegrationEvent> @events);
}

internal sealed class InMemoryEventBus(IServiceProvider _serviceProvider) : IEventBus
{
    public async Task Publish(IIntegrationEvent @event)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        Type handlerType = typeof(IIntegrationEventHandler<>)
            .MakeGenericType(@event.GetType());

        IEnumerable<object>? handlers = scope.ServiceProvider
            .GetService(typeof(IEnumerable<>)
            .MakeGenericType(handlerType)) as IEnumerable<object>;

        foreach (object handler in handlers ?? [])
        {
            MethodInfo? handleMethod = handlerType.GetMethod("Handle");
            if (handleMethod != null)
            {
                await (Task)handleMethod
                    .Invoke(handler, [@event, CancellationToken.None])!;
            }
        }
    }

    public async Task PublishMany(IEnumerable<IIntegrationEvent> @events)
    {
        foreach (IIntegrationEvent domainEvent in @events)
            await Publish(domainEvent);
    }
}
