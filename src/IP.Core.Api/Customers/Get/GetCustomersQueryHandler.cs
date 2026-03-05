namespace IP.Core.Api.Customers.Get;

internal class GetCustomersQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetCustomersQuery, GetCustomersResponse>
{
    private readonly ICustomerRepository _customerRepository =
        _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<GetCustomersResponse>> Handle(
        GetCustomersQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Customer, object>>> sortDictionary = new()
        {            
            { "name", x => x.Name.Value },
            { "tradingName", x => x.TradingName.Value! },           
        };

        GetCustomersRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<Customer> queryEmploye =
            _customerRepository.Entities.AsNoTracking();

        IQueryable<CustomerResponseData> Customers =
            ApplyUserFilters(queryRequest, queryEmploye)
            .OrderBy(
                sortDictionary,
                query.Request.OrderBy, ["name"])
            .Paginate(pageNumber, pageSize)
            .Select(employee => new CustomerResponseData(employee));

        int count = await queryEmploye.CountAsync(cancellationToken);

        GetCustomersResponse response = new(
            Customers,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<Customer> ApplyUserFilters(
        GetCustomersRequest queryRequest,
        IQueryable<Customer> query)
    {
        string normalizedName =
            queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;

        string normalizedTradingName =
            queryRequest?.TradingNameContains?.NormalizeCustom() ?? string.Empty;

        return query
            .WhereIf(normalizedName.IsNotNullOrWhiteSpace(), 
                query => query.Name.ValueNormalized.Contains(normalizedName))
            .WhereIf(normalizedTradingName.IsNotNullOrWhiteSpace(), 
                query => query.TradingName.ValueNormalized!.Contains(normalizedTradingName))
            .WhereIf(queryRequest!.PersonTypeIs.HasValue, query => 
                query.PersonTypeCode == queryRequest.PersonTypeIs)
            .WhereIf(queryRequest!.StatusIs.HasValue, query => 
                query.StatusCode == queryRequest.StatusIs);
    }
}