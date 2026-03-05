namespace IP.Shared.Domain.Entities;

public interface IEntityDeletable
{
    public const int REASON_MAX_LENGTH = 5000;
    public const int REASON_MIN_LENGTH = 10;

    EntityDeletableInfo DeletableInfo { get; }
}

public abstract class EntityDeletable<TId> : Entity<TId>, IEntityDeletable
{
    public EntityDeletableInfo DeletableInfo { get; private set; } = new();
}

public class EntityDeletableInfo
{
    public bool Deleted { get; private set; }

    public string? DeletedReason { get; private set; }

    public void RevertDelete()
    {
        Deleted = false;
        DeletedReason = null;
    }

    public void SetAsDeleted() => Deleted = true;

    public void SetReason(string reason) => DeletedReason = reason;
}