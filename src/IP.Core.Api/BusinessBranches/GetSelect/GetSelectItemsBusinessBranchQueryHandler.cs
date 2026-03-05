namespace IP.Core.Api.BusinessBranches.GetSelect;

internal sealed class GetSelectItemsBusinessBranchQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsBusinessBranchQuery, GetSelectItemsBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _BusinessBranchRepository =
       _unitOfWork.GetRepository<IBusinessBranchRepository>();

    public async Task<Result<GetSelectItemsBusinessBranchResponse>> Handle(
        GetSelectItemsBusinessBranchQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<BusinessBranch, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value! },
        };

        GetSelectItemsBusinessBranchQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<BusinessBranch> queryRoles =
            ApplyBusinessBranchFilters(searchTerm, _BusinessBranchRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsBusinessBranchResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<BusinessBranch> ApplyBusinessBranchFilters(
        string searchTerm,
        IQueryable<BusinessBranch> queryRoles)
    {
        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(searchTermTrimmed) ||
            query.Name.ValueNormalized.Contains(searchTermTrimmed));
    }
}