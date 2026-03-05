namespace IP.Core.Api.Employees.GetTeamById;

internal sealed class GetEmployeeTeamQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetEmployeeTeamQuery, GetEmployeeTeamResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<GetEmployeeTeamResponse>> Handle(
        GetEmployeeTeamQuery query,
        CancellationToken cancellationToken)
    {
        Dictionary<string, Expression<Func<Employee, object>>> sortDictionary = new()
        {
            { "active", x => x.ActivableInfo.Active },
            { "name", x => x.Name.Value! },
        };

        GetEmployeeTeamQueryRequest queryRequest = query.Request;

        int pageNumber = queryRequest.PageNumber ?? 1;
        int pageSize = queryRequest.PageSize ?? 10;

        string searchTerm = queryRequest.SearchTerm ?? string.Empty;

        IQueryable<Employee> queryRoles =
            _employeeRepository.Entities.AsNoTracking()
            .Where(x => x.Manager.Id == query.Id);

        IQueryable<ValueLabelItem> roles = queryRoles
            .OrderBy(x => x.Name.Value)
            .Paginate(pageNumber, pageSize)
            .Select(x => new ValueLabelItem(
                x.Id.ToString(),
                x.Name.Value,
                !x.ActivableInfo.Active));

        int count = await queryRoles.CountAsync(cancellationToken);

        GetEmployeeTeamResponse response = new(
            roles,
            new ResolvedDataPagination(pageNumber, pageSize, count));

        return Result.Success(response);
    }
}