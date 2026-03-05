namespace IP.IDI.Domain.Users;

public class AppUserRole : IdentityUserRole<Guid>
{

    public AppUserRole()
    {        
    }

    public virtual AppUser User { get; set; } = null!;
    public virtual AppRole Role { get; set; } = null!;

    public static AppUserRole Create(Guid userId, Guid roleId) =>
        new() { UserId = userId, RoleId = roleId };
}