namespace IP.IDI.Api.AppGuides.MarkAsReadByUser;

public sealed record MarkAsReadByUserCommand(string Id, MarkAsReadByUserRequest Request) :
    ICommand<MarkAsReadByUserResponse>;

public sealed class MarkAsReadByUserRequest
{
    public Guid UserId { get; set; }
}

public sealed class MarkAsReadByUserResponse(string message) :
    ResolvedData<object>(null, message);