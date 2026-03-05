namespace IP.Core.Api.Cities.GetCities;

internal class GetCitiesQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetCitiesQuery, GetCitiesResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<GetCitiesResponse>> Handle(
        GetCitiesQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<City, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value },
            { "stateName", x => x.State.Name.Value }
        };

        GetCitiesQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<City> queryCities =
            _cityRepository.Data().AsNoTracking();

        IQueryable<GetCitiesResponseData> cities =
            ApplyUserFilters(queryRequest, queryCities)
            .Include(x => x.State)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "stateName", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetCitiesResponseData(
                x.Id.Value,
                x.IBGECode,
                x.Name.Value,
                x.State.Name.Value ?? string.Empty,
                x.ActivableInfo.Active));

        int count = await queryCities.CountAsync(cancellationToken);

        GetCitiesResponse response = new(
            cities,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);

    }
    private static IQueryable<City> ApplyUserFilters(
        GetCitiesQueryRequest queryRequest,
        IQueryable<City> queryCities)
    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;
        string? ibgeCode = queryRequest?.IBGECodeContains?.NormalizeCustom() ?? string.Empty;

        return queryCities
            .Include(x => x.State)
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName) ||
                query.State.Name.ValueNormalized.Contains(normalizedName))
            .WhereIf(ibgeCode.IsNotNullOrWhiteSpace(),
                query => query.IBGECode.Contains(ibgeCode));
    }
}
