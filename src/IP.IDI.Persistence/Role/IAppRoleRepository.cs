namespace IP.IDI.Persistence.Role;

public interface IAppRoleRepository :
    ICreationRepository<AppRole>,
    IQueryableRepository<AppRole>,
    IUpdatableRepository<AppRole>,
    IDeletableRepository<AppRole>;