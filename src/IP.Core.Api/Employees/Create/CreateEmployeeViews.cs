namespace IP.Core.Api.Employees.Create;

public sealed record CreateEmployeeCommand(CreateEmployeeRequest Request) :
    ICommand<CreateEmployeeResponse>, ILoggableData;

public sealed class CreateEmployeeRequest
{
    [Required]
    [MinLength(Employee.NAME_MIN_LENGTH)]
    [MaxLength(Employee.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Description("Número de CPF em formato XXX.XXX.XXX-XX ou XXXXXXXXXXXX")]
    public string CPF { get; set; } = string.Empty;

    public CreateEmployeeUserRequest? User { get; set; } = default;
    public CreateEmployeeManagerRequest? Manager { get; set; } = default;
}

public sealed class CreateEmployeeManagerRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public sealed class CreateEmployeeUserRequest
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

public sealed class CreateEmployeeResponse(
    Employee employee,
    string message) :
    ResolvedData<EmployeeResponseData>(new EmployeeResponseData(employee), message);