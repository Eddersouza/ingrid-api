namespace IP.Shared.Persistence.Repositories;

public class RepositoryBase<TEntity>(DbContext appContext) where TEntity : class
{
    private readonly DbSet<TEntity> _DbSet = appContext.Set<TEntity>();

    public async Task Create(TEntity entity) => await _DbSet.AddAsync(entity);

    public void Delete(TEntity entity, bool forceRemoveFromDB = false)
    {
        if (entity is IEntityDeletable && !forceRemoveFromDB)
        {
            ((IEntityDeletable)entity)
                .DeletableInfo.SetAsDeleted();

            Update(entity);
            return;
        }

        _DbSet.Remove(entity);
    }

    public IQueryable<TEntity> Data() =>
        _DbSet;

    public IQueryable<TEntity> Entities =>
       _DbSet;

    public void Update(TEntity entity) =>
        _DbSet.Update(entity);
}