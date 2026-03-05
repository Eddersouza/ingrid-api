namespace IP.Core.Api.Addresses.GetAddresses;

internal class GetAddressesQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetAddressesQuery, GetAddressesResponse>
{
    private readonly IAddressRepository _addressRepository =
        _unitOfWork.GetRepository<IAddressRepository>();

    public async Task<Result<GetAddressesResponse>> Handle(
        GetAddressesQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Address, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value },
            { "city", x => x.City.Name.Value },
            { "state", x => x.City.State.Code },
        };

        GetAddressesQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<Address> queryAddresses =
            _addressRepository.Data()
                .AsNoTracking()
                .Include(x => x.City);

        if (queryRequest.CityId.HasValue)
        {
            queryAddresses = queryAddresses
                .Where(x => x.CityId == new CityId(queryRequest.CityId.Value));
        }

        IQueryable<GetAddressesResponseData> addresses =
            ApplyUserFilters(queryRequest, queryAddresses)
            .OrderBy(sortDictionary, query.Request.OrderBy, ["active:desc", "name", "city"])
            .Paginate(pageNumber, pageSize)
            .Select(x => new GetAddressesResponseData(
                x.Id.Value,
                new GetAddressCityData(
                    x.City.Id.Value.ToString(),
                    x.City.Name.Value ?? string.Empty),
                x.Neighborhood.Value ?? string.Empty,
                x.City.State.Code,
                x.Name.Value,
                x.Code,
                x.ActivableInfo.Active));

        int count = await queryAddresses.CountAsync(cancellationToken);

        GetAddressesResponse response = new(
            addresses,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);

    }
    private static IQueryable<Address> ApplyUserFilters(
        GetAddressesQueryRequest queryRequest,
        IQueryable<Address> queryAddresses)

    {
        string normalizedName = queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;

        return queryAddresses
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(),
                query => query.Name.ValueNormalized.Contains(normalizedName) ||
                query.City.Name.ValueNormalized.Contains(normalizedName));
    }
}
