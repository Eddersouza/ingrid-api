namespace IP.AccCust.Persistence.Customers;

public interface ICustomerRepository :
    IQueryableRepository<Customer>,
    ICreationRepository<Customer>,
    IUpdatableRepository<Customer>,
    IDeletableRepository<Customer>;

public sealed class CustomerRepository(DbContext appContext) :
    RepositoryBase<Customer>(appContext), ICustomerRepository;