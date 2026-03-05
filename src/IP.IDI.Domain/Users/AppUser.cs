namespace IP.IDI.Domain.Users;

public class AppUser : IdentityUser<Guid>,
    IEntity<Guid>,
    IEntityAuditable,
    IEntityDeletable,
    IEntityActivable
{
    public const int EMAIL_MAX_LENGTH = 256;
    public const int PASSWORD_MAX_LENGTH = 50;
    public const int PASSWORD_MIN_LENGTH = 8;
    public const int USERNAME_MAX_LENGTH = 256;
    public const int USERNAME_MIN_LENGTH = 5;

    public EntityActivableInfo ActivableInfo { get; private set; } = new();

    public EntityAuditableInfo AuditableInfo { get; private set; } = new();

    public EntityDeletableInfo DeletableInfo { get; private set; } = new();
    public EntityDomainEvents EventsInfo { get; private set; } = new();

    public virtual ICollection<AppUserRole> UserRoles { get; set; } = [];

    public static AppUser Create(
        string username,
        string email,
        string password)
    {
        AppUser user = new()
        {
            Id = Guid.CreateVersion7(),
            UserName = username,
            NormalizedUserName = username.NormalizeCustom(),
            Email = email,
            NormalizedEmail = email.NormalizeCustom(),
            PasswordHash = password,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            LockoutEnabled = true,
            EmailConfirmed = true
        };

        user.ActivableInfo.SetAsActive();

        return user;
    }

    public string ToEntityName() => "Usuario";

    public override string ToString() => $"{UserName} - {Email}";
}