namespace IP.Core.Api.Employees;

public sealed class EmployeeResponseData(Employee employee)
{
    public Guid Id { get; private set; } = employee.Id.Value;
    public string Name { get; private set; } = employee.Name.Value;
    public string Cpf { get; private set; } = employee.CPF.ValueFormated;
    public bool Active { get; private set; } = employee.ActivableInfo.Active;
    public DateTime CreationDate { get; private set; } = employee.AuditableInfo.CreatedDate;
    

    public EmployeeManagerResponseData? Manager { get; private set; } =
        employee.Manager?.Id is not null
        ? new EmployeeManagerResponseData(employee.Manager.Id!.Value, employee.Manager.Name)
        : null;


    public EmployeeUserResponseData? User { get; private set; } =
        employee.User?.Id is not null
        ? new EmployeeUserResponseData(employee.User.Id!.Value, employee.User.Name)
        : null;
}

public sealed class EmployeeUserResponseData(Guid id, string name)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
}

public sealed class EmployeeManagerResponseData(Guid id, string name)
{
    public Guid Id { get; private set; } = id;
    public string Name { get; private set; } = name;
}