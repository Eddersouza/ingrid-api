namespace IP.AccIPInfo.Api.IntegratorSystems.GetSelect;

internal sealed class GetSelectItemsIntegratorSystemQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsIntegratorSystemQuery, GetSelectItemsIntegratorSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
       _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<GetSelectItemsIntegratorSystemResponse>> Handle(
        GetSelectItemsIntegratorSystemQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<IntegratorSystem, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value! },
        };

        GetSelectItemsIntegratorSystemQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<IntegratorSystem> queryRoles =
            ApplyIntegratorSystemFilters(searchTerm, _integratorSystemRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["active", "name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsIntegratorSystemResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<IntegratorSystem> ApplyIntegratorSystemFilters(
        string searchTerm,
        IQueryable<IntegratorSystem> queryRoles)
    {
        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(searchTermTrimmed));
    }
}