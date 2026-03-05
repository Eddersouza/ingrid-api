namespace IP.Shared.Persistence.Interceptors;

internal sealed class UpdateAuditableInterceptor(
    ICurrentSessionProvider _currentSessionProvider) :
    SaveChangesInterceptor
{
    private DbContext _context = null!;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
           DbContextEventData eventData,
           InterceptionResult<int> result,
           CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            _context = eventData.Context;
            CreateAudity();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static bool CanAuditEntity(EntityEntry entry) =>
        !(entry.State is EntityState.Detached or EntityState.Unchanged);

    private static void SetAuditableData(EntityEntry entry, string userNameEmail)
    {
        if (entry.State == EntityState.Added)
            ((IEntityAuditable)entry.Entity).AuditableInfo.
                AddCreation(userNameEmail, DateTime.UtcNow);

        if (entry.State == EntityState.Modified)
            ((IEntityAuditable)entry.Entity).AuditableInfo
                .AddUpdate(userNameEmail, DateTime.UtcNow);
    }

    private void CreateAudity()
    {
        IEnumerable<EntityEntry> entities = GetTypedEntityChanged<IEntityAuditable>();

        foreach (var entry in entities)
        {
            if (!CanAuditEntity(entry)) continue;

            SetAuditableData(entry, _currentSessionProvider.CurrentUserInfo.UserNameEmail);
        }
    }

    private IEnumerable<EntityEntry> GetTypedEntityChanged<TEntity>() where TEntity : class
        => _context.ChangeTracker.Entries().Where(x => x.Entity is TEntity);
}