namespace IP.Shared.Abstractions.Sessions;

public interface ICurrentSessionProvider
{
    ICurrentUserInfo CurrentUserInfo { get; }
    Guid RequestId { get; }
}

internal class CurrentSessionProvider(ICurrentUserInfo currentUserInfo) : 
    ICurrentSessionProvider
{
    public ICurrentUserInfo CurrentUserInfo { get; } = currentUserInfo;
    public Guid RequestId { get; } = Guid.CreateVersion7();
}