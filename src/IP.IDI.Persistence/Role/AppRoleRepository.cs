namespace IP.IDI.Persistence.Role;

internal class AppRoleRepository(IDIDbContext appContext) :
    RepositoryBase<AppRole>(appContext),
    IAppRoleRepository;