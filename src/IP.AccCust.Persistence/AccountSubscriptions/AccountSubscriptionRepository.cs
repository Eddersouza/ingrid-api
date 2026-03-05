namespace IP.AccCust.Persistence.AccountSubscriptions;
public interface IAccountSubscriptionRepository :
    IQueryableRepository<AccountSubscription>,
    ICreationRepository<AccountSubscription>,
    IUpdatableRepository<AccountSubscription>,
    IDeletableRepository<AccountSubscription>;

internal sealed class AccountSubscriptionRepository(AccIPExtDbContext appContext) :
    RepositoryBase<AccountSubscription>(appContext),
    IAccountSubscriptionRepository;
