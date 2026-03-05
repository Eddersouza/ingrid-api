namespace IP.Core.Api.Employees.GetByUserId;

public sealed record GetEmployeeByUserQuery(Guid UserId) :
    IQuery<GetEmployeeByUserResponse?>;

public sealed class GetEmployeeByUserResponse(
    Employee? employee) :
    ResolvedData<EmployeeResponseData>(
        employee is not null ? new EmployeeResponseData(employee) : null, string.Empty);