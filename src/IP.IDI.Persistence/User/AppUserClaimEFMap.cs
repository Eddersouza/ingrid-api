namespace IP.IDI.Persistence.User;

public class AppUserClaimEFMap : IEntityTypeConfiguration<AppUserClaim>
{
    public void Configure(EntityTypeBuilder<AppUserClaim> builder)
    {
        builder.ToTable(DBConstants.AppUserClaimsTable);

        builder.HasKey(uc => uc.Id);
    }
}