namespace IP.IDI.Persistence.Seerders;

internal abstract class RoleBaseData
{
    public AppRole Role { get; set; } = null!;
    public ICollection<AppRoleClaim> RoleClaims { get; set; } = [];

    public AppRoleClaim CreateRoleClaim(string value) => new()
    {
        RoleId = Role.Id,
        ClaimType = JwtCustomClaimNames.Permission,
        ClaimValue = value,        
    };
}
