using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace IP.Shared.Domain.Entities;

public class AuditEntry(
    Guid requestId,
    Guid userId,
    string userName,
    EntityEntry entry)
{
    private EntityEntry entry = entry;
    private string MaskPattern = "***********";
    public AuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = [];
    public EntityEntry Entry { get => entry; set => entry = value; }
    public Dictionary<string, object> KeyValues { get; } = [];
    public Dictionary<string, object> NewValues { get; } = [];
    public Dictionary<string, object> OldValues { get; } = [];
    public Guid RequestId { get; set; } = requestId;
    public string TableName { get; set; } = entry.Entity.GetType().Name;
    public Guid UserId { get; set; } = userId;
    public string UserName { get; set; } = userName;

    public void GetPropertiesInfo()
    {
        foreach (var property in entry.Properties)
        {
            string propertyName = property.Metadata.Name;

            object? currentValue = property.CurrentValue;
            object? originalValue = property.OriginalValue;

            if (property.Metadata.IsPrimaryKey())
            {
                AddPrimaryKey(propertyName, currentValue);
                continue;
            }

            HandlePropertyByState(propertyName, currentValue, originalValue, property.IsModified);
        }
    }

    public AuditTrail ToAudit()
    {
        var audit = new AuditTrail
        {
            Id = Guid.CreateVersion7(),
            UserId = UserId,
            RequestId = RequestId,
            UserName = UserName,
            Type = AuditType.ToString(),
            TableName = TableName,
            DateTime = DateTime.Now,
            PrimaryKey = JsonSerializer.Serialize(KeyValues)
        };

        if (OldValues.Count > 0)
            audit.OldValues = JsonSerializer.Serialize(OldValues);
        if (NewValues.Count > 0)
            audit.NewValues = JsonSerializer.Serialize(NewValues);
        if (ChangedColumns.Count > 0)
            audit.AffectedColumns = JsonSerializer.Serialize(ChangedColumns);

        return audit;
    }

    private void AddPrimaryKey(
        string propertyName,
        object? currentValue)
    {
        KeyValues[propertyName] = currentValue!;
    }

    private void HandlePropertyByState(
        string propertyName,
        object? currentValue,
        object? originalValue,
        bool isModified)
    {
        switch (Entry.State)
        {
            case EntityState.Added:
                SetCreate(propertyName, currentValue);
                break;

            case EntityState.Deleted:
                SetDelete(propertyName, originalValue);
                break;

            case EntityState.Modified:
                SetUpdate(propertyName, currentValue, originalValue, isModified);
                break;
        }
    }

    private void SetCreate(
        string propertyName,
        object? currentValue)
    {
        AuditType = AuditType.Create;
        NewValues[propertyName] = currentValue!;
    }

    private void SetDelete(string propertyName, object? originalValue)
    {
        AuditType = AuditType.Delete;
        OldValues[propertyName] = originalValue!;
    }

    private void SetUpdate(
        string propertyName,
        object? currentValue,
        object? originalValue,
        bool isModified)
    {
        if (!isModified) return;
        if (Equals(
            originalValue?.ToString(),
            currentValue?.ToString())) return;


        bool maskValuesproperty = propertyName == "PasswordHash" ||
            propertyName.Contains("Password") ||
            propertyName.Contains("password") ||
            propertyName.Contains("token") ||
            propertyName.Contains("Token");

        originalValue = maskValuesproperty ? MaskPattern : originalValue;
        currentValue = maskValuesproperty ? MaskPattern : currentValue;

        AuditType = AuditType.Update;
        ChangedColumns.Add(propertyName);
        OldValues[propertyName] = originalValue!;
        NewValues[propertyName] = currentValue!;
    }
}

public class AuditTrail
{
    public string? AffectedColumns { get; set; }
    public DateTime DateTime { get; set; }
    public Guid Id { get; set; }
    public string? NewValues { get; set; }
    public string? OldValues { get; set; }
    public string PrimaryKey { get; set; } = string.Empty;
    public Guid RequestId { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
}

public enum AuditType
{
    None = 0,
    Create = 1,
    Update = 2,
    Delete = 3
}