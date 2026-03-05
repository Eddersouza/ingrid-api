namespace IP.IDI.Api.Users.Delete;

public sealed record DeleteUserCommand(Guid Id, DeleteUserRequest Request) :
    ICommand<DeleteUserResponse>, ILoggableData;

public sealed class DeleteUserRequest {

    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
};

public sealed class DeleteUserResponse(string message) :
    ResolvedData<object>(null, message);