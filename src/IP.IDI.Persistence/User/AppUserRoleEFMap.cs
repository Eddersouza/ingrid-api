namespace IP.IDI.Persistence.User;

public class AppUserRoleEFMap : IEntityTypeConfiguration<AppUserRole>
{
    public void Configure(EntityTypeBuilder<AppUserRole> builder)
    {
        builder.ToTable(DBConstants.AppUserRolesTable);

        builder.HasKey(r => new { r.UserId, r.RoleId });

        builder.HasOne(x => x.User).WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.UserId);
        builder.HasOne(x => x.Role).WithMany(x => x.UserRoles)
            .HasForeignKey(x => x.RoleId);
    }
}