namespace IP.IDI.Persistence.AppGuides;

public interface IAppGuidesRepository :
    IQueryableRepository<AppGuide>,
    IUpdatableRepository<AppGuide>,
    ICreationRepository<AppGuide>,
    IDeletableRepository<AppGuide>;

internal sealed class AppGuidesRepository(IDIDbContext appContext) :
    RepositoryBase<AppGuide>(appContext),
    IAppGuidesRepository;