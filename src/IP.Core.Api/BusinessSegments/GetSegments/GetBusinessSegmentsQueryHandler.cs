namespace IP.Core.Api.BusinessSegments.GetSegments;

internal class GetBusinessSegmentsQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetBusinessSegmentsQuery, GetBusinessSegmentsResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<GetBusinessSegmentsResponse>> Handle(
        GetBusinessSegmentsQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<BusinessSegment, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "segmentName", x => x.SegmentName.Value },
            { "businessBranch", x => x.BusinessBranch.Name.Value }
        };

        GetBusinessSegmentsQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        Guid businessBranchId = queryRequest.BusinessBranchId!.Value;

        IQueryable<BusinessSegment> queryBusinessSegments =
            _businessSegmentRepository.Data()
                .AsNoTracking()
                .Include(x => x.BusinessBranch)
                .Where(x => x.BusinessBranchId == new BusinessBranchId(businessBranchId));

        IQueryable<GetBusinessSegmentsResponseData> segments =
            ApplyUserFilters(queryRequest, queryBusinessSegments)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "segmentName", "businessBranch"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetBusinessSegmentsResponseData(
                x.Id.Value,
                x.BusinessBranch.Name.Value ?? string.Empty,
                x.SegmentName.Value,
                x.ActivableInfo.Active));

        int count = await queryBusinessSegments.CountAsync(cancellationToken);

        GetBusinessSegmentsResponse response = new(
            segments,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);

    }
    private static IQueryable<BusinessSegment> ApplyUserFilters(
        GetBusinessSegmentsQueryRequest queryRequest,
        IQueryable<BusinessSegment> queryBusinessSegments)

    {
        string normalizedName = queryRequest?.SegmentNameContains?.NormalizeCustom() ?? string.Empty;

        return queryBusinessSegments
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.SegmentName.ValueNormalized.Contains(normalizedName) ||
                query.BusinessBranch.Name.ValueNormalized.Contains(normalizedName));
    }
}
