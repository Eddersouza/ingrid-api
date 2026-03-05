namespace IP.AccCust.Persistence.AccountMovementsView;

internal class AccountMovementEFMap : IEntityTypeConfiguration<AccountMovementView>
{
    public void Configure(EntityTypeBuilder<AccountMovementView> builder)
    {
        builder.HasNoKey();
        builder.ToView(DBConstants.AccountMovementView, DBConstants.Schema);

        builder.Property(x => x.TransactionId)
            .HasColumnName("transaction_id")
            .IsRequired();

        builder.Property(x => x.CreatedAt)
           .HasColumnName("transaction_created_at")
           .IsRequired();
    }
}
