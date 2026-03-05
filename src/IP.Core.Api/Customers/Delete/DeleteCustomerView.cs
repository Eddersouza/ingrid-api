namespace IP.Core.Api.Customers.Delete;

public sealed record DeleteCustomerCommand(Guid Id, DeleteCustomerRequest Request) :
    ICommand<DeleteCustomerResponse>, ILoggableData;

public sealed class DeleteCustomerRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
};

public sealed class DeleteCustomerResponse(string message) :
    ResolvedData<object>(null, message);