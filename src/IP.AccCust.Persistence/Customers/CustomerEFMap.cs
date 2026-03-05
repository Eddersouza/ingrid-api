namespace IP.AccCust.Persistence.Customers;

public sealed class CustomerEFMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(DBConstantsCore.CustomerTable, DBConstantsCore.Schema);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<CustomerId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.OwnsOne(entity =>
            entity.Name, entity =>
            {
                entity.Property(prop => prop.Value)
                     .HasMaxLength(Customer.NAME_MAX_LENGTH)
                     .IsRequired(true);

                entity.Property(prop => prop.ValueNormalized)
                     .HasMaxLength(Customer.NAME_MAX_LENGTH)
                     .IsRequired(true);
            });

        builder.OwnsOne(entity =>
            entity.TradingName, entity =>
            {
                entity.Property(prop => prop.Value)
                     .HasMaxLength(Customer.TRADINGNAME_MAX_LENGTH)
                     .IsRequired(false);

                entity.Property(prop => prop.ValueNormalized)
                     .HasMaxLength(Customer.TRADINGNAME_MAX_LENGTH)
                     .IsRequired(false);
            });

        builder.OwnsOne(entity =>
            entity.DocumentNumber, entity =>
            {
                entity.Property(prop => prop.Value)
                   .HasMaxLength(CNPJ.LENGTH)
                   .IsRequired(true);
                entity.Ignore(prop => prop.ValueFormated);
            });

        builder.Property(entity => entity.PersonTypeCode)
            .HasConversion<string>()
            .HasMaxLength(Customer.PERSONTYPE_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(entity => entity.StatusCode)
            .HasConversion<string>()
            .HasMaxLength(Customer.STATUS_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(entity => entity.Remarks)
            .HasColumnType($"text({Customer.REMARKS_MAX_LENGTH})")
            .IsRequired(false);

        builder.Property(entity => entity.ExternalId)
            .HasMaxLength(Customer.EXTERNAL_ID_MAX_LENGTH)
            .IsRequired(false);

        builder.AddBaseEntityFields();
    }
}