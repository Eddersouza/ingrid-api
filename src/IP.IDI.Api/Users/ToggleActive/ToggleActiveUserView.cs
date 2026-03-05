namespace IP.IDI.Api.Users.ToggleActive;

public sealed record ToggleActiveUserCommand(Guid Id, ToggleActiveUserRequest Request) :
    ICommand<ToggleActiveUserResponse>, ILoggableData;

public sealed class ToggleActiveUserRequest
{
    [Required]
    public bool Active { get; set; }

    [Required]
    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class ToggleActiveUserResponse(
    UserResponseData data,
    string message) : ResolvedData<UserResponseData>(data, message);