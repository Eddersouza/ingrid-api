namespace IP.Core.Api.Employees.Get;

internal class GetEmployeesQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetEmployeesQuery, GetEmployeesResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<GetEmployeesResponse>> Handle(
        GetEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Employee, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value },
        };

        GetEmployeesRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        IQueryable<Employee> queryEmploye =
            _employeeRepository.Entities.AsNoTracking();

        IQueryable<EmployeeResponseData> employees =
            ApplyUserFilters(queryRequest, queryEmploye)
            .OrderBy(
                sortDictionary,
                query.Request.OrderBy, ["active:desc", "name"])
            .Paginate(pageNumber, pageSize)
            .Select(employee => new EmployeeResponseData(employee));

        int count = await queryEmploye.CountAsync(cancellationToken);

        GetEmployeesResponse response = new(
            employees,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }

    private static IQueryable<Employee> ApplyUserFilters(
        GetEmployeesRequest queryRequest,
        IQueryable<Employee> query)
    {
        string normalizedName =
            queryRequest?.NameContains?.NormalizeCustom() ?? string.Empty;

        return query.WhereIf(
            normalizedName.IsNotNullOrWhiteSpace(),
            query => query.Name.ValueNormalized.Contains(normalizedName));
    }
}