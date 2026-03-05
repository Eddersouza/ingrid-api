namespace IP.AccCust.Persistence.AccountMovements;

internal sealed class AccountMovementSummaryEFMap : IEntityTypeConfiguration<AccountMovementSummary>
{
    public void Configure(EntityTypeBuilder<AccountMovementSummary> builder)
    {
        builder.ToTable(DBConstantsIP.AccountIPMovementsSummaryTable, DBConstantsIP.Schema);

        builder.HasKey(ac => ac.Id);

        builder.Property(ac => ac.Id)
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(ac => ac.AccountIPId)
            .HasConversion(new EntityIdValueConverter<AccountIPId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(ac => ac.AccountNumber)
            .IsRequired(true);

        builder.Ignore(ac => ac.AccountNumberTemp);

        builder.Property(ac => ac.CancelAmount)
            .HasPrecision(
                NumericConstants.DefaultDecimalPrecision,
                 NumericConstants.DefaultDecimalScale)
            .IsRequired(true);

        builder.Property(ac => ac.CancelQuantity)
            .IsRequired(true);

        builder.Property(ac => ac.MovementDateHour)
            .IsRequired(true);

        builder.Property(ac => ac.ReturnAmount)
            .HasPrecision(
                NumericConstants.DefaultDecimalPrecision,
                 NumericConstants.DefaultDecimalScale)
            .IsRequired(true);

        builder.Property(ac => ac.ReturnQuantity)
            .IsRequired(true);

        builder.Property(ac => ac.SettledAmount)
            .HasPrecision(
                NumericConstants.DefaultDecimalPrecision,
                 NumericConstants.DefaultDecimalScale)
            .IsRequired(true);

        builder.Property(ac => ac.SettledQuantity)
            .IsRequired(true);

        builder.Property(ac => ac.ReturnAmountParcial)
            .HasPrecision(
                NumericConstants.DefaultDecimalPrecision,
                 NumericConstants.DefaultDecimalScale)
            .IsRequired(true);

        builder.Property(ac => ac.ReturnQuantityParcial)
            .IsRequired(true);
        
        builder.HasOne(ac => ac.AccountIP)
            .WithMany()
            .HasForeignKey(ac => ac.AccountIPId);

        builder.AddBaseEntityFields();
    }
}