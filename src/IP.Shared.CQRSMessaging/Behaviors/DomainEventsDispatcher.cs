namespace IP.Shared.CQRSMessaging.Behaviors;

public sealed class DomainEventsDispatcher(IServiceProvider serviceProvider)
{
    public async Task DispatchAsync(
        IEnumerable<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (IDomainEvent domainEvent in domainEvents)
        {
            using IServiceScope scope = serviceProvider.CreateScope();

            Type handlerType = typeof(IDomainEventHandler<>)
                .MakeGenericType(domainEvent.GetType());

            IEnumerable<object>? handlers = scope.ServiceProvider
                .GetService(typeof(IEnumerable<>)
                .MakeGenericType(handlerType)) as IEnumerable<object>;

            foreach (object handler in handlers ?? [])
            {
                MethodInfo? handleMethod = handlerType.GetMethod("Handle");
                if (handleMethod != null)
                {
                    await (Task)handleMethod
                        .Invoke(handler, [domainEvent, cancellationToken])!;
                }
            }
        }
    }
}