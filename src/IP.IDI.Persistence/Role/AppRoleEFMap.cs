namespace IP.IDI.Persistence.Role;

public class AppRoleEFMap : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.ToTable(DBConstants.AppRolesTable);

        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.NormalizedName).IsUnique();

        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

        builder.Property(u => u.Name).HasMaxLength(AppRole.NAME_MAX_LENGTH);
        builder.Property(u => u.NormalizedName).HasMaxLength(AppRole.NAME_MAX_LENGTH);

        builder.HasMany<AppUserRole>().WithOne()
            .HasForeignKey(ur => ur.RoleId).IsRequired();
     
        builder.AddBaseEntityFields();
    }
}