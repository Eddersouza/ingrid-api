namespace IP.Shared.Domain.Entities;

public abstract class EntityAuditableDeletableActivable<TId> : 
    Entity<TId>, 
    IEntityAuditable, 
    IEntityDeletable, 
    IEntityActivable
{
    public EntityActivableInfo ActivableInfo { get; private set; } = new();
    public EntityAuditableInfo AuditableInfo { get; private set; } = new();
    public EntityDeletableInfo DeletableInfo { get; private set; } = new();
}