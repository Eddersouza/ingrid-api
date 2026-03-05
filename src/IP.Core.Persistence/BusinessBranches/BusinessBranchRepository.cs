using IP.Core.Domain.BusinessBranches;

namespace IP.Core.Persistence.BusinessBranches;

public interface IBusinessBranchRepository :
    IQueryableRepository<BusinessBranch>,
    ICreationRepository<BusinessBranch>,
    IUpdatableRepository<BusinessBranch>,
    IDeletableRepository<BusinessBranch>;

internal sealed class BusinessBranchRepository(CoreDbContext appContext) : 
    RepositoryBase<BusinessBranch>(appContext),
    IBusinessBranchRepository;

