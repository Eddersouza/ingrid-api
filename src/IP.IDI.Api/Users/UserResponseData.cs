namespace IP.IDI.Api.Users;

public sealed record UserResponseData(
    Guid Id,
    string User,
    string Email,
    UserRoleResponseData Role,
    bool Active,
    string CreatedDate,
    bool EmailConfirmed);

public sealed record UserRoleResponseData(Guid Id, string Name);