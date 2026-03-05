namespace IP.IDI.Persistence.Seerders;

internal abstract class UserBaseData
{
    public AppUser User { get; set; } = null!;
    public AppRole Role { get; set; } = null!;

    public AppUserRole UserRole => new()
    {
        UserId = User.Id,
        RoleId = Role.Id
    };  
}
