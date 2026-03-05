namespace IP.IDI.Persistence.User;

internal class AppUserRepository(IDIDbContext appContext) :
    RepositoryBase<AppUser>(appContext),
    IAppUserRepository;