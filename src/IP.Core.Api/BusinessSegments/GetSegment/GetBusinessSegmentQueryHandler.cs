namespace IP.Core.Api.BusinessSegments.GetSegment;

internal sealed class GetBusinessSegmentQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetBusinessSegmentQuery, GetBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<GetBusinessSegmentResponse>> Handle(
        GetBusinessSegmentQuery query,
        CancellationToken cancellationToken)
    {
        BusinessSegmentId id = new(query.Id);
        BusinessSegment? currentSegment =
            await _businessSegmentRepository.Data()
            .Include(s => s.BusinessBranch.Name)
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentSegment is null) return BusinessSegmentErrors.BusinessSegmentNotFound;

        GetBusinessSegmentResponse response = new(
            currentSegment.Id.Value!,
            currentSegment.SegmentName.Value!,
            currentSegment.BusinessBranchId.Value!,
            currentSegment.BusinessBranch.Name.Value!,
            currentSegment.ActivableInfo.Active);

        return Result.Success(response);
    }
}
