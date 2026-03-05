namespace IP.Shared.Persistence.Repositories;

public interface IDeletableRepository<in TEntity>
    : IRepository where TEntity : class
{
    void Delete(TEntity entity, bool forceRemoveFromDB = false);
}