namespace IP.IDI.Api.Users.Confirm;

public sealed record ConfirmUserCommand(Guid Id, ConfirmUserRequest Request) :
    ICommand<ConfirmUserResponse>, ILoggableData;

public sealed class ConfirmUserRequest
{  
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class ConfirmUserResponse(UserResponseData data, string message) :
    ResolvedData<UserResponseData>(data, message);