namespace IP.Core.Api.Employees.ToggleActive;

public sealed record ToggleActiveEmployeeCommand(Guid Id, ToggleActiveEmployeeRequest Request) :
    ICommand<ToggleActiveEmployeeResponse>, ILoggableData;

public sealed class ToggleActiveEmployeeRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ToggleActiveEmployeeResponse(
    Employee employee,
    string message) :
    ResolvedData<EmployeeResponseData>(new EmployeeResponseData(employee), message);