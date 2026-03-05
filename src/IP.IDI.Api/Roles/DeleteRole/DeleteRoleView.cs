namespace IP.IDI.Api.Roles.DeleteRole;

public sealed record DeleteRoleCommand(Guid Id, DeleteRoleRequest Request) :
    ICommand<DeleteRoleResponse>, ILoggableData;

public sealed class DeleteRoleRequest {

    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
};

public sealed class DeleteRoleResponse(string message) :
    ResolvedData<object>(null, message);