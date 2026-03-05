namespace IP.IDI.Api.Users.RequestChangePassword;

public sealed record RequestChangePasswordCommand(Guid Id) :
    ICommand<RequestChangePasswordResponse>, ILoggableData;


public sealed class RequestChangePasswordResponse(
    RequestChangePasswordUserResponse? data, string message) :
    ResolvedData<RequestChangePasswordUserResponse>(data, message);

public sealed record RequestChangePasswordUserResponse(Guid Id, string User, string Email);
