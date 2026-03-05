namespace IP.Shared.Persistence.Interceptors;

internal sealed class CreateAuditTrailInterceptor(
    ICurrentSessionProvider _currentSessionProvider) :
    SaveChangesInterceptor
{
    private DbSet<AuditTrail> _auditTrails = null!;
    private DbContext _context = null!;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            _context = eventData.Context;
            _auditTrails = _context.Set<AuditTrail>();
            CreateAuditTrails();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void CreateAuditTrails()
    {
        List<AuditTrail> auditsToSave = [];
        var entities = _context.ChangeTracker.Entries()
            .Where(x => x.Entity is IEntityAuditable);

        foreach (var entry in entities)
        {
            if (entry.Entity is AuditTrail ||
                entry.State is EntityState.Detached or EntityState.Unchanged)
                continue;

            AuditEntry auditEntry = new(
                _currentSessionProvider.RequestId,
                _currentSessionProvider.CurrentUserInfo.Id,
                _currentSessionProvider.CurrentUserInfo.UserNameEmail,
                entry);

            auditEntry.GetPropertiesInfo();

            auditsToSave.Add(auditEntry.ToAudit());
        }

        _auditTrails.AddRange(auditsToSave);
    }
}