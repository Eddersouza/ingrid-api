namespace IP.Core.Api.Employees.GetById;

internal sealed class GetEmployeeQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetEmployeeQuery, GetEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<GetEmployeeResponse>> Handle(
        GetEmployeeQuery query,
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(query.Id);

        Employee? currentRecord = await _employeeRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(employee =>
                employee.Id == employeeId,
                cancellationToken);

        if (currentRecord is null) return EmployeeErrors.NotFound;

        return Result.Success(new GetEmployeeResponse(currentRecord!));
    }
}