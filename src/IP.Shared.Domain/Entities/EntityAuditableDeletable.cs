namespace IP.Shared.Domain.Entities;

public abstract class EntityAuditableDeletable<TId> : 
    Entity<TId>, 
    IEntityAuditable, 
    IEntityDeletable
{
    public EntityAuditableInfo AuditableInfo { get; private set; } = new();
    public EntityDeletableInfo DeletableInfo { get; private set; } = new();
}