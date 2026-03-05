namespace IP.Shared.Persistence.Repositories;

public interface IUpdatableRepository<in TEntity> :
    IRepository where TEntity : class
{
    void Update(TEntity entity);
}