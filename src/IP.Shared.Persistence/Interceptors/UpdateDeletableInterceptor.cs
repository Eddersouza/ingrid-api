namespace IP.Shared.Persistence.Interceptors;

internal sealed class UpdateDeletableInterceptor : SaveChangesInterceptor
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
            SetAsDeleted();
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static bool HasDeleted(EntityEntry entry) =>
        entry.State is EntityState.Deleted;

    private IEnumerable<EntityEntry> GetTypedEntityChanged<TEntity>() where TEntity : class =>
        _context.ChangeTracker.Entries().Where(x => x.Entity is TEntity);

    private void SetAsDeleted()
    {
        IEnumerable<EntityEntry> entities =
            GetTypedEntityChanged<IEntityDeletable>();

        foreach (var entry in entities)
        {
            if (!HasDeleted(entry)) continue;

            ((IEntityDeletable)entry.Entity).DeletableInfo.SetAsDeleted();
        }
    }
}