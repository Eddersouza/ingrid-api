namespace IP.IDI.Domain.Roles;

public class AppRoleClaim : IdentityRoleClaim<Guid>
{
    public virtual AppRole Role { get; set; } = null!;
}