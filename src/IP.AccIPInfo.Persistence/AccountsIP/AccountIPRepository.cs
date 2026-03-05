
namespace IP.AccIPInfo.Persistence.AccountsIP;

public interface IAccountIPRepository :
    IQueryableRepository<AccountIP>,
    ICreationRepository<AccountIP>,
    IUpdatableRepository<AccountIP>;

internal sealed class AccountIPRepository(
    AccIPDbContext appContext) : 
    RepositoryBase<AccountIP>(appContext),
    IAccountIPRepository;
