namespace IP.IDI.Api.Users.Authorize;
public sealed record AuthorizeUserCommand() :
    ICommand<AuthorizeUserResponse>, ILoggableData;

public sealed record AuthorizeUserResponseData(
    Guid RoleId,
    string RoleName,
    List<string> Permissions);

public sealed class AuthorizeUserResponse(
    Guid roleId,
    string roleName,
    List<string> permissions,
    string message) :
    ResolvedData<AuthorizeUserResponseData>(
        new AuthorizeUserResponseData(
            roleId,
            roleName,
            permissions),
        message);