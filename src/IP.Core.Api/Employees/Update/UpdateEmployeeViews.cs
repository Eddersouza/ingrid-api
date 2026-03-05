namespace IP.Core.Api.Employees.Update;

public sealed record UpdateEmployeeCommand(Guid Id, UpdateEmployeeRequest Request) :
    ICommand<UpdateEmployeeResponse>, ILoggableData;

public sealed class UpdateEmployeeRequest
{
    [Required]
    [MinLength(Employee.NAME_MIN_LENGTH)]
    [MaxLength(Employee.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Description("Número de CPF em formato XXX.XXX.XXX-XX ou XXXXXXXXXXXX")]
    public string CPF { get; set; } = string.Empty;
    
    public UpdateEmployeeUserRequest? User { get; set; } = default;

    public UpdateEmployeeManagerRequest? Manager { get; set; } = default;
}

public sealed class UpdateEmployeeManagerRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public sealed class UpdateEmployeeUserRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}


public sealed class UpdateEmployeeResponse(
    Employee employee,
    string message) :
    ResolvedData<EmployeeResponseData>(new EmployeeResponseData(employee), message);