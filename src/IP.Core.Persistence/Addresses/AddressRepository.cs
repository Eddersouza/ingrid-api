namespace IP.Core.Persistence.Addresses;

public interface IAddressRepository :
    IQueryableRepository<Address>,
    ICreationRepository<Address>,
    IUpdatableRepository<Address>;
internal sealed class AddressRepository(CoreDbContext appContext) :
    RepositoryBase<Address>(appContext),
    IAddressRepository;
