namespace IP.Core.Persistence.Cities;

public interface ICityRepository :
    IQueryableRepository<City>,
    ICreationRepository<City>,
    IUpdatableRepository<City>,
    IDeletableRepository<City>;
internal sealed class CityRepository(CoreDbContext appContext) :
    RepositoryBase<City>(appContext),
    ICityRepository;
