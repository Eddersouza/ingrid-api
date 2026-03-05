namespace IP.Shared.Persistence.Repositories;

public interface IQueryableRepository<TEntity> :
    IRepository where TEntity : class
{
    IQueryable<TEntity> Data();

    IQueryable<TEntity> Entities { get; }
}