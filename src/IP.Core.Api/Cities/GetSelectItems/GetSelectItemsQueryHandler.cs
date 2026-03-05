namespace IP.Core.Api.Cities.GetSelectItems;

internal sealed class GetSelectItemsQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsCitiesQuery, GetSelectItemsCitiesResponse>
{
    private readonly ICityRepository _cityRepository =
       _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<GetSelectItemsCitiesResponse>> Handle(
        GetSelectItemsCitiesQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<City, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name!.Value! },
        };

        GetSelectItemsCitiesQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<City> queryCities =
            ApplyUserFilters(searchTerm, _cityRepository.Data().AsNoTracking())
            .Include(c => c.State)
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> cities = queryCities
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                $"{x.Name.Value} - {x.State.Code}",
                !x.ActivableInfo.Active));

        int count = await queryCities.CountAsync(cancellationToken);

        GetSelectItemsCitiesResponse response = new(
            cities,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<City> ApplyUserFilters(
        string searchTerm,
        IQueryable<City> queryCities) =>
        queryCities.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name!.Value!.ToLower().Contains(searchTerm.ToLower()));
}
