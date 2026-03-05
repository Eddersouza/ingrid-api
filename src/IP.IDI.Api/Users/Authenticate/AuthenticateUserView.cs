namespace IP.IDI.Api.Users.Authenticate;

public sealed record AuthenticateUserCommand(AuthenticateUserRequest Request) :
    ICommand<AuthenticateUserResponse>, ILoggableData;

public class AuthenticateUserRequest
{
    [Required]
    [MinLength(AppUser.USERNAME_MIN_LENGTH)]
    [MaxLength(AppUser.USERNAME_MAX_LENGTH)]
    public string UserOrEmail { get; set; } = string.Empty;

    [Required]
    [MinLength(AppUser.PASSWORD_MIN_LENGTH)]
    [MaxLength(AppUser.PASSWORD_MAX_LENGTH)]
    [MaskFieldInLog]
    public string Password { get; set; } = string.Empty;
}

public sealed record AuthenticateUserResponseData(Guid Id, string User, string Email, string Role, string Token);

public sealed class AuthenticateUserResponse(AuthenticateUserResponseData data, string message) :
    ResolvedData<AuthenticateUserResponseData>(data, message);