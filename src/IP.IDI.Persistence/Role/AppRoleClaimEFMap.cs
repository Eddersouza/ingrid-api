namespace IP.IDI.Persistence.Role;

public class AppRoleClaimEFMap : IEntityTypeConfiguration<AppRoleClaim>
{
    public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
    {
        builder.ToTable(DBConstants.AppRoleClaimsTable);

        builder.HasKey(r => r.Id);

        builder.HasOne(rc => rc.Role)
            .WithMany(r => r.RoleClaims)
            .HasForeignKey(rc => rc.RoleId);
    }
}