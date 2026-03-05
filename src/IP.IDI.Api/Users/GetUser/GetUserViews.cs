namespace IP.IDI.Api.Users.GetUser;

public sealed record GetUserQuery(Guid Id) :
    IQuery<GetUserResponse>;

public sealed class GetUserResponse(
    UserResponseData data) :
    ResolvedData<UserResponseData>(data, string.Empty);