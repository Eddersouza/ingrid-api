namespace IP.Core.Api.Customers.GetSelect;

internal sealed class GetSelectItemsCustomerQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsCustomerQuery, GetSelectItemsCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository =
       _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<GetSelectItemsCustomerResponse>> Handle(
        GetSelectItemsCustomerQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Customer, object>>> sortDictionary = new()
        {            
            { "name", x => x.Name.Value! },
            { "tradingname", x => x.TradingName.Value!}
        };

        GetSelectItemsCustomerQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<Customer> queryRoles =
            ApplyCustomerFilters(searchTerm, _customerRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                x.StatusCode.Equals(CustomerStatusEnum.Inactive)));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsCustomerResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<Customer> ApplyCustomerFilters(
        string searchTerm,
        IQueryable<Customer> queryRoles)
    {
        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(searchTermTrimmed) ||
            query.TradingName.ValueNormalized!.Contains(searchTermTrimmed));
    }
}