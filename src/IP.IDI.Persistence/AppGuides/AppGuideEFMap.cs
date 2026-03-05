namespace IP.IDI.Persistence.AppGuides;

internal class AppGuideEFMap : IEntityTypeConfiguration<AppGuide>
{
    public void Configure(EntityTypeBuilder<AppGuide> builder)
    {
        builder.ToTable(DBConstants.AppGuidesTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new EntityIdValueConverter<AppGuideId>())
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(AppGuide.NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.LinkId)
            .HasMaxLength(AppGuide.LINK_ID_MAX_LENGTH)
            .IsRequired();

        builder.AddBaseEntityFields();
    }
}
