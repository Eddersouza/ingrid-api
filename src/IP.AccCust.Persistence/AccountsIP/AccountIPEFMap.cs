namespace IP.AccCust.Persistence.AccountsIP;

internal class AccountIPEFMap : IEntityTypeConfiguration<AccountIP>
{
    public void Configure(EntityTypeBuilder<AccountIP> builder)
    {
        builder.ToTable(DBConstantsIP.AccountIPTable, DBConstantsIP.Schema);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<AccountIPId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(s => s.Number)
            .IsRequired(true);

        builder.Property(s => s.StatusCode)
            .HasMaxLength(AccountIP.STATUS_MAX_LENGTH)
            .HasConversion<string>()
            .IsRequired(true);

        builder.Property(s => s.TypeCode)
            .HasMaxLength(AccountIP.TYPE_MAX_LENGTH)
            .HasConversion<string>()
            .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Alias, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(AccountIPAlias.MAX_LENGTH)
                       .IsRequired(false);

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(AccountIPAlias.MAX_LENGTH)
                       .IsRequired(false);
              });

        builder.OwnsOne(entity =>
            entity.BusinessBranchSegment, entity =>
            {
                entity.Property(prop => prop.BranchId)
                     .IsRequired(false);

                entity.Property(prop => prop.BranchName)
                     .HasMaxLength(AccountIPBusinessBranchSegment.BRANCH_NAME_MAX_LENGTH)
                     .IsRequired(false);

                entity.Property(prop => prop.SegmentId)
                    .IsRequired(false);

                entity.Property(prop => prop.SegmentName)
                     .HasMaxLength(AccountIPBusinessBranchSegment.SEGMENT_NAME_MAX_LENGTH)
                     .IsRequired(false);
            });

        builder.OwnsOne(entity =>
             entity.Customer, entity =>
             {
                 entity.Property(prop => prop.Id)
                    .IsRequired(false);

                 entity.Property(prop => prop.Name)
                    .HasMaxLength(AccountIPCustomer.MAX_LENGTH)
                    .IsRequired(true);

                 entity.Property(prop => prop.NameNormalized)
                    .HasMaxLength(AccountIPCustomer.MAX_LENGTH)
                    .IsRequired(true);
             });

        builder.OwnsOne(entity =>
            entity.Integrator, entity =>
            {
                entity.Property(prop => prop.Id)
                    .IsRequired(false);

                entity.Property(prop => prop.Name)
                     .HasMaxLength(AccountIPIntegratorSistem.MAX_LENGTH)
                     .IsRequired(false);

                entity.Property(prop => prop.NameNormalized)
                     .HasMaxLength(AccountIPIntegratorSistem.MAX_LENGTH)
                     .IsRequired(false);
            });

        builder.OwnsOne(entity =>
            entity.Owner, entity =>
            {
                entity.Property(prop => prop.Id)
                    .IsRequired(false);

                entity.Property(prop => prop.Name)
                    .HasMaxLength(AccountIPOwner.MAX_LENGTH)
                    .IsRequired(false);

                entity.Property(prop => prop.NameNormalized)
                    .HasMaxLength(AccountIPOwner.MAX_LENGTH)
                    .IsRequired(false);

                entity.Property(prop => prop.OwnerIsIP)
                    .IsRequired(false);
            });

        builder.OwnsOne(entity =>
            entity.Retailer, entity =>
            {
                entity.Property(prop => prop.Id)
                    .IsRequired(false);

                entity.Property(prop => prop.Name)
                    .HasMaxLength(AccountIPRetailer.MAX_LENGTH)
                    .IsRequired(false);

                entity.Property(prop => prop.NameNormalized)
                    .HasMaxLength(AccountIPRetailer.MAX_LENGTH)
                    .IsRequired(false);
            });

        builder.OwnsOne(entity =>
            entity.Subscription, entity =>
            {
                entity.Property(prop => prop.Id)
                    .IsRequired(false);

                entity.Property(prop => prop.Name)
                    .HasMaxLength(AccountIPAccountSubscription.MAX_LENGTH)
                    .IsRequired(false);

                entity.Property(prop => prop.NameNormalized)
                    .HasMaxLength(AccountIPAccountSubscription.MAX_LENGTH)
                    .IsRequired(false);
            });

        builder.Ignore(s => s.StatusDescription);
        builder.Ignore(s => s.TypeDescription);

        builder.AddBaseEntityFields();
    }
}