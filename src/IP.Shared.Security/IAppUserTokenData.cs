namespace IP.Shared.Security;

public interface IAppUserTokenData
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Profile { get; init; }
    public IEnumerable<string> Claims { get; init; }
}