namespace IP.IDI.Persistence.AppGuides;

internal class AppGuideViewedEFMap : IEntityTypeConfiguration<AppGuideViewed>
{
    public void Configure(EntityTypeBuilder<AppGuideViewed> builder)
    {
        builder.ToTable(DBConstants.AppGuideViewedTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new EntityIdValueConverter<AppGuideViewedId>())
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.AppGuide)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.AppGuideId)
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .IsRequired();

        builder.AddBaseEntityFields();
    }
}
