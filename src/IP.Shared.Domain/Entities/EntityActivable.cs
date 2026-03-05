namespace IP.Shared.Domain.Entities;

public interface IEntityActivable
{
    public const int REASON_MAX_LENGTH = 5000;
    public const int REASON_MIN_LENGTH = 10;

    EntityActivableInfo ActivableInfo { get; }
}
public abstract class EntityActivable<TId> : Entity<TId>, IEntityActivable
{
    public EntityActivableInfo ActivableInfo { get; private set; } = new();
}

public class EntityActivableInfo
{
    public bool Active { get; private set; }

    public string? InativeReason { get; private set; }

    public void SetAsActive()
    {
        Active = true;
        InativeReason = null;
    }

    public void SetAsDeactive(string? reason = null)
    {
        Active = false;
        InativeReason = reason;
    }
}
