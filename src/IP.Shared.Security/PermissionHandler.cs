namespace IP.Shared.Security;

public class PermissionRequirement(params string[] permissions) :
    IAuthorizationRequirement
{
    public string[] Permissions { get; } = permissions;
}

internal class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == JwtCustomClaimNames.Permission))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        bool hasPermission = context.User.Claims
            .Any(x =>
                x.Type == JwtCustomClaimNames.Permission &&
                requirement.Permissions.Contains(x.Value));

        if (hasPermission)
            context.Succeed(requirement);
        else
            context.Fail();

        return Task.CompletedTask;
    }
}