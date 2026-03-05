
namespace IP.AccIPInfo.Persistence.AccountSubscriptions;
public interface IAccountSubscriptionRepository :
    IQueryableRepository<AccountSubscription>,
    ICreationRepository<AccountSubscription>,
    IUpdatableRepository<AccountSubscription>,
    IDeletableRepository<AccountSubscription>;

internal sealed class AccountSubscriptionRepository(AccIPDbContext appContext) :
    RepositoryBase<AccountSubscription>(appContext),
    IAccountSubscriptionRepository;
