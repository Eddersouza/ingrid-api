namespace IP.Core.Persistence.Addresses;

internal class AddressEFMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(DBConstants.AddressTable);

        builder.HasKey(s => s.Id);

        builder.HasOne(s => s.City)
            .WithMany()
            .HasForeignKey(s => s.CityId)
            .IsRequired(true);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<AddressId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(s => s.Code)
            .HasMaxLength(Address.CODE_MAX_LENGTH)
            .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(Address.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(Address.NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.OwnsOne(entity =>
              entity.Neighborhood, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(Address.NEIGHBORHOOD_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(Address.NEIGHBORHOOD_MAX_LENGTH)
                       .IsRequired();
              });

        builder.AddBaseEntityFields();
    }
}