using IP.Shared.Abstractions.Auths;
using Microsoft.Extensions.Primitives;

namespace IP.Shared.Abstractions.Sessions;

public interface ICurrentUserInfo
{
    string Email { get; }
    Guid Id { get; }
    bool IsAuthenticated { get; }
    string Name { get; }
    List<string> Permissions { get; }
    string ProfileCode { get; }

    string UserNameEmail { get; }

    string GetJwtToken();

    bool HasPermission(string permission);

    void RevertAtUser();

    void SetServiceAsUser(Guid id, string user);
}

internal class CurrentUserInfo(IHttpContextAccessor _accessor) : ICurrentUserInfo
{
    private bool _isService = false;

    private string? _serviceEmail = null;

    private Guid? _serviceId = null;

    private string? _serviceName = null;

    public string Email => GetEmail();

    public Guid Id => GetId();

    public bool IsAuthenticated =>
        _accessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

    public string Name => GetName();

    public List<string> Permissions =>
            _accessor.HttpContext?.User?.Claims?
        .Where(x => x.Type == JwtCustomClaimNames.Permission)?
        .Select(x => x.Value)
        .ToList() ?? [];

    public string ProfileCode =>
            _isService ?
        "service" :
        _accessor.HttpContext?.User?.GetUserProfile() ??
        throw new ArgumentNullException(
            "CurrentUser.ProfileCode",
            "Current Service Email must not be null");

    public string UserNameEmail => $"{Name} - {Email}";

    public string GetJwtToken()
    {
        // Access the current HttpContext via the accessor
        var httpContext = _accessor.HttpContext;

        if (httpContext == null)
        {
            return string.Empty;
        }

        // Try to get the "Authorization" header
        if (httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authHeaders))
        {
            var bearerToken = authHeaders.ToString();

            // Check if the header starts with "Bearer " and extract the token part
            if (bearerToken.StartsWith("Bearer "))
            {
                // Return the token without the "Bearer " prefix
                return bearerToken["Bearer ".Length..].Trim();
            }
        }

        return string.Empty;
    }

    public bool HasPermission(string permission) => Permissions.Contains(permission);

    public void RevertAtUser() =>
            (_isService, _serviceId, _serviceName, _serviceEmail) =
        (false, null, null, null);

    public void SetServiceAsUser(Guid id, string user) =>
        (_isService, _serviceId, _serviceName, _serviceEmail) =
        (true, id, user, $"{user}@ip.com.br");

    private string GetEmail()
    {
        string? email = _isService ?
            _serviceEmail :
            _accessor.HttpContext?.User?.GetUserEmail();

        return email ?? "anonimous@anonimous.com.br";
    }

    private Guid GetId()
    {
        Guid? id = _isService ?
            _serviceId :
            _accessor.HttpContext?.User?.GetUserId();

        return id ?? Guid.Empty;
    }

    private string GetName()
    {
        string? name = _isService ?
            _serviceName :
            _accessor.HttpContext?.User?.GetUserName();

        return name ?? "Anonimous";
    }
}