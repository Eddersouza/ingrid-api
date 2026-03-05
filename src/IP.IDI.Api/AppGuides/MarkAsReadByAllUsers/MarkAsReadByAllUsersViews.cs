namespace IP.IDI.Api.AppGuides.MarkAsReadByAllUsers;

public sealed record MarkAsReadByAllUsersCommand(string Id) :
    ICommand<MarkAsReadByAllUsersResponse>;

public sealed class MarkAsReadByAllUsersResponse(string message) :
    ResolvedData<object>(null, message);
