namespace IP.Shared.Abstractions.Sessions;

internal static class ClaimsPrincipalExtensions
{
    public static string? GetUserEmail(this ClaimsPrincipal? principal) =>
        principal.GetValueClaim(JwtRegisteredClaimNames.Email);

    public static Guid? GetUserId(this ClaimsPrincipal? principal) =>
        Guid.TryParse(principal.GetValueClaim(JwtRegisteredClaimNames.Sub),
            out Guid parsedUserId) ?
            parsedUserId : null;

    public static string? GetUserName(this ClaimsPrincipal? principal) =>
       principal.GetValueClaim(JwtRegisteredClaimNames.Name);

    public static string? GetUserProfile(this ClaimsPrincipal? principal) =>
               principal.GetValueClaim(JwtRegisteredClaimNames.Profile);

    private static string? GetValueClaim(this ClaimsPrincipal? principal, string claimType)
    {
        string? claimValue = principal?.FindFirst(claimType)?.Value;
        return claimValue ??
            null;
    }
}