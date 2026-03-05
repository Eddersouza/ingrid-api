namespace IP.Core.Api.Employees.GetSelect;

internal sealed class GetSelectItemsEmployeeQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetSelectItemsEmployeeQuery, GetSelectItemsEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
       _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<GetSelectItemsEmployeeResponse>> Handle(
        GetSelectItemsEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Employee, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value! },
        };

        GetSelectItemsEmployeeQueryRequest queryRequest = request.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<Employee> queryRoles =
            ApplyEmployeeFilters(searchTerm, _employeeRepository.Entities.AsNoTracking())
            .OrderBy(sortDictionary, queryRequest.OrderBy, ["name"]);

        IQueryable<ValueLabelItem> roles = queryRoles
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetSelectItemsEmployeeResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<Employee> ApplyEmployeeFilters(
        string searchTerm,
        IQueryable<Employee> queryRoles)
    {
        var searchTermTrimmed = searchTerm.Trim().NormalizeCustom();
        return queryRoles.WhereIf(searchTerm.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(searchTermTrimmed) ||
            query.User.NameNormalized.Contains(searchTermTrimmed));
    }
}