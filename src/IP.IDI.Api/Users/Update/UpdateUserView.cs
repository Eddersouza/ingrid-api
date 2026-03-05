namespace IP.IDI.Api.Users.Update;

public record UpdateUserCommand(Guid Id, UpdateUserRequest Request) :
    ICommand<UpdateUserResponse>, ILoggableData;

public sealed class UpdateUserRequest
{
    [Required]
    public Guid? RoleId { get; set; }
}

public sealed class UpdateUserResponse(UserResponseData data, string message) :
    ResolvedData<UserResponseData>(data, message);