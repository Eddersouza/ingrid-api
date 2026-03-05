namespace IP.Core.Api.Employees.GetById;

public sealed record GetEmployeeQuery(Guid Id) :
    IQuery<GetEmployeeResponse>;

public sealed class GetEmployeeResponse(
    Employee employee) :
    ResolvedData<EmployeeResponseData>(
        new EmployeeResponseData(employee), string.Empty);