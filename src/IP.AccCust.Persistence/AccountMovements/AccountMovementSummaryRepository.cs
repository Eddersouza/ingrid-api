namespace IP.AccIPInfo.Persistence.AccountMovements;

public interface IAccountMovementSummaryRepository :
    IQueryableRepository<AccountMovementSummary>,
    ICreationRepository<AccountMovementSummary>;

internal sealed class AccountMovementSummaryRepository(AccIPExtDbContext appContext) :
    RepositoryBase<AccountMovementSummary>(appContext),
    IAccountMovementSummaryRepository;