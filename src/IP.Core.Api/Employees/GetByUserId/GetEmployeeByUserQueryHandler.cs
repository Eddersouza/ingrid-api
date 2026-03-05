namespace IP.Core.Api.Employees.GetByUserId;

internal sealed class GetEmployeeByUserQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetEmployeeByUserQuery, GetEmployeeByUserResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<GetEmployeeByUserResponse>> Handle(
        GetEmployeeByUserQuery query,
        CancellationToken cancellationToken)
    {
        Employee? currentRecord = await _employeeRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(employee =>
                employee.User.Id == query.UserId,
                cancellationToken);

        return Result.Success(new GetEmployeeByUserResponse(currentRecord!));
    }
}