namespace IP.IDI.Domain.Roles;

public class AppRole : IdentityRole<Guid>,
    IEntity<Guid>,
    IEntityAuditable,
    IEntityDeletable,
    IEntityActivable
{
    public const int NAME_MAX_LENGTH = 100;
    public const int NAME_MIN_LENGTH = 5;
    public EntityActivableInfo ActivableInfo { get; private set; } = new();
    public EntityAuditableInfo AuditableInfo { get; private set; } = new();
    public EntityDeletableInfo DeletableInfo { get; private set; } = new();
    public EntityDomainEvents EventsInfo { get; private set; } = new();
    public virtual ICollection<AppRoleClaim> RoleClaims { get; set; } = [];
    public virtual ICollection<AppUserRole> UserRoles { get; set; } = [];
    public static AppRole Create(string name)
    {
        var role = new AppRole
        {
            Name = name,
            NormalizedName = name.NormalizeCustom()
        };

        role.ActivableInfo.SetAsActive();

        return role;
    }

    public string ToEntityName() => "Perfil";

    public override string ToString() => Name!;
}