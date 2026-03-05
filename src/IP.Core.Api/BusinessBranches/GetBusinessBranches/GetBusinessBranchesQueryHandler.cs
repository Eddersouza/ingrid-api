namespace IP.Core.Api.BusinessBranches.GetBusinessBranches;

internal class GetBusinessBranchesQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetBusinessBranchesQuery, GetBusinessBranchesResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();

    public async Task<Result<GetBusinessBranchesResponse>> Handle(
        GetBusinessBranchesQuery query, 
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<BusinessBranch, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value }
        };

        GetBusinessBranchesQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<BusinessBranch> queryBusinessBranches =
            _businessBranchRepository.Data().AsNoTracking();

        IQueryable<GetBusinessBranchesResponseData> businessBranches =
            ApplyUserFilters(queryRequest, queryBusinessBranches)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetBusinessBranchesResponseData(
                x.Id.Value,
                x.Name.Value!,
                x.ActivableInfo.Active));

        int count = await queryBusinessBranches.CountAsync(cancellationToken);

        GetBusinessBranchesResponse response = new(
            businessBranches,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<BusinessBranch> ApplyUserFilters(
        GetBusinessBranchesQueryRequest queryRequest,
        IQueryable<BusinessBranch> queryBusinessBranches)
    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;
        return queryBusinessBranches
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName));
    }
}
