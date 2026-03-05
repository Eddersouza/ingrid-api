namespace IP.AccIPInfo.Persistence.IntegratorSystems;

internal class IntegratorSystemEFMap : IEntityTypeConfiguration<IntegratorSystem>
{
    public void Configure(EntityTypeBuilder<IntegratorSystem> builder)
    {
        builder.ToTable(DBConstants.IntegratorSystemTable);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<IntegratorSystemId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(IntegratorSystem.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(IntegratorSystem.NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.Property(s => s.SiteUrl)
            .HasMaxLength(IntegratorSystem.SITE_URL_MAX_LENGTH)
            .IsRequired(false);

        builder.Property(s => s.Description)
            .HasMaxLength(IntegratorSystem.DESCRIPTION_MAX_LENGTH)
            .IsRequired(false);

        builder.AddBaseEntityFields();
    }
}
