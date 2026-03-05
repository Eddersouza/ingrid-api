namespace IP.IDI.Persistence.User;

public interface IAppUserRepository : 
    IUpdatableRepository<AppUser>,
    ICreationRepository<AppUser>, 
    IQueryableRepository<AppUser>,
    IDeletableRepository<AppUser>;