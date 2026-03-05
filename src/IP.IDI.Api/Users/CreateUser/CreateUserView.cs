namespace IP.IDI.Api.Users.CreateUser;

public record CreateUserCommand(CreateUserRequest Request) :
    ICommand<CreateUserResponse>, ILoggableData;

public sealed class CreateUserRequest
{
    [Required]
    [MinLength(AppUser.USERNAME_MIN_LENGTH)]
    [MaxLength(AppUser.USERNAME_MAX_LENGTH)]
    public string User { get; set; } = string.Empty;
    
    [Required]
    [MinLength(AppUser.USERNAME_MIN_LENGTH)]
    [MaxLength(AppUser.USERNAME_MAX_LENGTH)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public Guid? RoleId { get; set; }
}

public sealed class CreateUserResponse(UserResponseData data, string message) :
    ResolvedData<UserResponseData>(data, message);