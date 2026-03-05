namespace IP.Core.Api.Employees.Delete;

public sealed record DeleteEmployeeCommand(Guid Id, DeleteEmployeeRequest Request) :
    ICommand<DeleteEmployeeResponse>, ILoggableData;

public sealed class DeleteEmployeeRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
};

public sealed class DeleteEmployeeResponse(string message) :
    ResolvedData<object>(null, message);