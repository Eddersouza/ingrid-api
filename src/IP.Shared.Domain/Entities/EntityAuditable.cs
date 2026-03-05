namespace IP.Shared.Domain.Entities;

public interface IEntityAuditable
{
    public const int USER_MAX_LENGTH = 256;

    EntityAuditableInfo AuditableInfo { get; }
}

public abstract class EntityAuditable<TId> : Entity<TId>, IEntityAuditable
{
    public EntityAuditableInfo AuditableInfo { get; private set; } = new();
}

public class EntityAuditableInfo
{
    public string CreatedBy { get; private set; } = string.Empty;
    public DateTime CreatedDate { get; private set; } = DateTime.Now;
    public string? UpdatedBy { get; private set; }
    public DateTime? UpdatedDate { get; private set; }

    public void AddCreation(string createdBy, DateTime createdDate)
    {
        CreatedBy = createdBy;
        CreatedDate = createdDate;
    }

    public void AddUpdate(string updatedBy, DateTime updatedDate)
    {
        UpdatedBy = updatedBy;
        UpdatedDate = updatedDate;
    }
}