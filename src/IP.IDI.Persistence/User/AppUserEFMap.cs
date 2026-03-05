namespace IP.IDI.Persistence.User
{
    public class AppUserEFMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable(DBConstants.AppUsersTable);

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.NormalizedUserName);
            builder.HasIndex(u => u.NormalizedEmail);

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.UserName)
                .HasMaxLength(AppUser.USERNAME_MAX_LENGTH);
            builder.Property(u => u.NormalizedUserName)
                .HasMaxLength(AppUser.USERNAME_MAX_LENGTH);
            builder.Property(u => u.Email)
                .HasMaxLength(AppUser.EMAIL_MAX_LENGTH);
            builder.Property(u => u.NormalizedEmail)
                .HasMaxLength(AppUser.EMAIL_MAX_LENGTH);


            builder.HasMany<AppUserClaim>().WithOne()
                .HasForeignKey(uc => uc.UserId).IsRequired();

            builder.HasMany<AppUserLogin>().WithOne()
                .HasForeignKey(ul => ul.UserId).IsRequired();

            builder.HasMany<AppUserToken>().WithOne()
                .HasForeignKey(ut => ut.UserId).IsRequired();

            builder.HasMany<AppUserRole>().WithOne()
                .HasForeignKey(ur => ur.UserId).IsRequired();

            builder.AddBaseEntityFields();
        }
    }
}