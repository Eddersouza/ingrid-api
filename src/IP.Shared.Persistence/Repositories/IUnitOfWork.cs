namespace IP.Shared.Persistence.Repositories;

public interface IUnitOfWork : IDisposable
{
    TRepository GetRepository<TRepository>() where TRepository : IRepository;

    void SaveChanges();

    Task SaveChangesAsync(CancellationToken cancellationToken);
}