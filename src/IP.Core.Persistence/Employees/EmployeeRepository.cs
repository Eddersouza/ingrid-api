namespace IP.Core.Persistence.Employees;

public interface IEmployeeRepository :
    IQueryableRepository<Employee>,
    ICreationRepository<Employee>,
    IUpdatableRepository<Employee>,
    IDeletableRepository<Employee>;

internal sealed class EmployeeRepository(CoreDbContext appContext) :
    RepositoryBase<Employee>(appContext),
    IEmployeeRepository;