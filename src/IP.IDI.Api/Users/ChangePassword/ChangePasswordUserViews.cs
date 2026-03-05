namespace IP.IDI.Api.Users.ChangePassword;

public sealed record ChangePasswordCommand(Guid Id, ChangePasswordRequest Request) :
    ICommand<ChangePasswordResponse>, ILoggableData;

public sealed class ChangePasswordRequest
{  
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class ChangePasswordResponse(UserResponseData data, string message) :
    ResolvedData<UserResponseData>(data, message);