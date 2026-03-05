namespace IP.AccIPInfo.Persistence.IntegratorSystems;

public interface IIntegratorSystemRepository :
    IQueryableRepository<IntegratorSystem>,
    ICreationRepository<IntegratorSystem>,
    IUpdatableRepository<IntegratorSystem>,
    IDeletableRepository<IntegratorSystem>;

internal sealed class IntegratorSystemRepository(AccIPDbContext appContext) :
    RepositoryBase<IntegratorSystem>(appContext),
    IIntegratorSystemRepository;