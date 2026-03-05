namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystems;

internal class GetSystemsQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetSystemsQuery, GetSystemsResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<GetSystemsResponse>> Handle(
        GetSystemsQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<IntegratorSystem, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value }
        };

        GetSystemsQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<IntegratorSystem> querySystems =
            _integratorSystemRepository.Data().AsNoTracking();

        IQueryable<GetSystemsResponseData> systems =
            ApplyUserFilters(queryRequest, querySystems)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetSystemsResponseData(
                x.Id.Value,
                x.Name.Value!,
                x.SiteUrl,
                x.Description,
                x.ActivableInfo.Active));

        int count = await querySystems.CountAsync(cancellationToken);

        GetSystemsResponse response = new(
            systems,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<IntegratorSystem> ApplyUserFilters(
        GetSystemsQueryRequest queryRequest,
        IQueryable<IntegratorSystem> querySystems)
    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;
        return querySystems
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName));
    }
}