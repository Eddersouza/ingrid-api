namespace IP.IDI.Persistence.User;

public class AppUserTokenEFMap : IEntityTypeConfiguration<AppUserToken>
{
    public void Configure(EntityTypeBuilder<AppUserToken> builder)
    {
        builder.ToTable(DBConstants.AppUserTokensTable);

        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });

        builder.Property(t => t.LoginProvider).HasMaxLength(AppUserToken.LOGIN_PROVIDER_MAX_LENGTH);
        builder.Property(t => t.Name).HasMaxLength(AppUserToken.NAME_MAX_LENGTH);
    }
}