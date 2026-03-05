namespace IP.IDI.Persistence.User;

public class AppUserLoginEFMap : IEntityTypeConfiguration<AppUserLogin>
{
    public void Configure(EntityTypeBuilder<AppUserLogin> builder)
    {
        builder.ToTable(DBConstants.AppUserLoginsTable);

        builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });

        builder.Property(l => l.LoginProvider)
            .HasMaxLength(AppUserLogin.LOGIN_PROVIDER_MAX_LENGTH);
        builder.Property(l => l.ProviderKey)
            .HasMaxLength(AppUserLogin.LOGIN_PROVIDER_MAX_LENGTH);
    }
}