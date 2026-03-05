namespace IP.Shared.Domain.Entities;

public abstract class EntityAuditableActivable<TId> :
    Entity<TId>,
    IEntityAuditable,
    IEntityActivable
{
    public EntityActivableInfo ActivableInfo { get; private set; } = new();
    public EntityAuditableInfo AuditableInfo { get; private set; } = new();
}