namespace IP.AccIPInfo.Persistence.AccountSubscriptions;

internal class AccountSubscriptionEFMap : IEntityTypeConfiguration<AccountSubscription>
{
    public void Configure(EntityTypeBuilder<AccountSubscription> builder)
    {
        builder.ToTable(DBConstants.AccountSubscriptionTable);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<AccountSubscriptionId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(s => s.ExternalId).
            HasMaxLength(AccountSubscription.EXTERNAL_ID_MAX_LENGTH)
            .IsRequired(false);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(AccountSubscription.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(AccountSubscription.NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.AddBaseEntityFields();
    }
}
