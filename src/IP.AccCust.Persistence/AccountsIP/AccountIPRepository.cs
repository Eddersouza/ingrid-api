namespace IP.AccCust.Persistence.AccountsIP;

public interface IAccountIPRepository :
    IQueryableRepository<AccountIP>,
    ICreationRepository<AccountIP>,
    IUpdatableRepository<AccountIP>;

internal sealed class AccountIPRepository(
    AccIPExtDbContext appContext) : 
    RepositoryBase<AccountIP>(appContext),
    IAccountIPRepository;
