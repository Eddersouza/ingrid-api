namespace IP.Core.Api.BusinessSegments.GetSelect;

internal sealed class GetSelectItemsBusinessSegmentQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsBusinessSegmentQuery, GetSelectItemsBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _BusinessSegmentRepository =
       _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<GetSelectItemsBusinessSegmentResponse>> Handle(
        GetSelectItemsBusinessSegmentQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<BusinessSegment, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.SegmentName.Value! },
        };

        GetSelectItemsBusinessSegmentQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<BusinessSegment> queryRoles =
            ApplyBusinessSegmentFilters(searchTerm, _BusinessSegmentRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.SegmentName.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsBusinessSegmentResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<BusinessSegment> ApplyBusinessSegmentFilters(
        string searchTerm,
        IQueryable<BusinessSegment> queryRoles)
    {
        if (Guid.TryParse(searchTerm, out Guid branchGuid))
        {
            BusinessBranchId branchId = BusinessBranchId.Create(branchGuid);
            return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.BusinessBranchId == branchId);
        }

        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.SegmentName.ValueNormalized.Contains(searchTermTrimmed) ||
            query.SegmentName.ValueNormalized.Contains(searchTermTrimmed));
    }
}