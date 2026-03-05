using IP.Shared.CQRSMessaging.Behaviors;
using IP.Shared.CQRSMessaging.Messages;

namespace IP.Shared.Persistence.Interceptors;

internal sealed class DispatcherEventInterceptor : SaveChangesInterceptor
{
    private readonly DomainEventsDispatcher _domainEventsDispatcher;
    public DispatcherEventInterceptor(
        DomainEventsDispatcher domainEventsDispatcher)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }
    private DbContext _context = null!;

    public override async ValueTask<int> SavedChangesAsync(
        SaveChangesCompletedEventData eventData,
        int result,        
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            _context = eventData.Context;
            await DispatchEvents();
        }
        return await new ValueTask<int>(result);
    }

    private async Task DispatchEvents()
    {
        List<IDomainEvent> events = [.. _context.ChangeTracker.Entries()
            .Where(x => x.Entity is IEntityDomainEvents)
            .SelectMany(entity =>
            {
                var domainEntity = (IEntityDomainEvents)entity.Entity;
                List<IDomainEvent> domainEvents = 
                    domainEntity.EventsInfo.DomainEvents;

                domainEntity.EventsInfo.ClearDomainEvents();

                return domainEvents;
            })];

       await _domainEventsDispatcher.DispatchAsync(events);
    }
}