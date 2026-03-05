using IP.Core.Domain.BusinessSegments;

namespace IP.Core.Persistence.BusinessSegments;

public interface IBusinessSegmentRepository :
    IQueryableRepository<BusinessSegment>,
    ICreationRepository<BusinessSegment>,
    IUpdatableRepository<BusinessSegment>,
    IDeletableRepository<BusinessSegment>;

internal sealed class BusinessSegmentRepository(CoreDbContext appContext) :
    RepositoryBase<BusinessSegment>(appContext),
    IBusinessSegmentRepository;

