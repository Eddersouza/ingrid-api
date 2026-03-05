namespace IP.Core.Persistence.Cities;

internal class CityEFMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable(DBConstants.CityTable);

        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.State)
            .WithMany()
            .HasForeignKey(s => s.StateId)
            .IsRequired(true);


        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<CityId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(s => s.IBGECode)
          .HasMaxLength(City.IBGE_CODE_MAX_LENGTH)
          .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(City.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(City.NAME_MAX_LENGTH)
                       .IsRequired();
              });          

        builder.AddBaseEntityFields();
    }
}
