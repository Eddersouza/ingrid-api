namespace IP.AccIPInfo.Persistence.AccountMovements;

public interface IAccountMovementSummaryRepository :
    IQueryableRepository<AccountMovementSummary>,
    ICreationRepository<AccountMovementSummary>;

internal sealed class AccountMovementSummaryRepository(AccIPDbContext appContext) :
    RepositoryBase<AccountMovementSummary>(appContext),
    IAccountMovementSummaryRepository;