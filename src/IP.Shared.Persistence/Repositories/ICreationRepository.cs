namespace IP.Shared.Persistence.Repositories;

public interface ICreationRepository<in TEntity> :
    IRepository where TEntity : class
{
    Task Create(TEntity entity);
}