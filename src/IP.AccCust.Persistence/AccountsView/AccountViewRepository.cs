namespace IP.AccCust.Persistence.AccountsView;

public interface IAccountViewRepository : IQueryableRepository<AccountView>;

internal sealed class AccountViewRepository(AccCustDbContext appContext) :
    RepositoryBase<AccountView>(appContext), IAccountViewRepository;