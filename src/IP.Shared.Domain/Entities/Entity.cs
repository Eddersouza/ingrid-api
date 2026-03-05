using IP.Shared.CQRSMessaging.Messages;

namespace IP.Shared.Domain.Entities;

public interface IEntity : IEntityDomainEvents
{
    abstract string ToEntityName();
};

public interface IEntity<TKey> : IEntity
{
    TKey Id { get; set; }
};

public interface IEntityDomainEvents
{
    EntityDomainEvents EventsInfo { get; }
}

public abstract class Entity<TKey> : IEntity<TKey>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public EntityDomainEvents EventsInfo { get; private set; } = new();
    public TKey Id { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    public abstract string ToEntityName();
};

public class EntityDomainEvents
{
    private readonly List<IDomainEvent> _domainEvents = [];

    public List<IDomainEvent> DomainEvents => [.. _domainEvents];

    public void ClearDomainEvents() => _domainEvents.Clear();

    public void Raise(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
}