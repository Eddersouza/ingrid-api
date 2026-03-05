namespace IP.AccCust.Persistence.AccountsView;

internal sealed class AccountViewEFMap : IEntityTypeConfiguration<AccountView>
{
    public void Configure(EntityTypeBuilder<AccountView> builder)
    {
        builder.HasNoKey();
        builder.ToView(DBConstants.AccountView, DBConstants.Schema);

        builder.Property(x => x.AccountNumber)
            .HasColumnName("account_number")
            .IsRequired();
        builder.Property(x => x.PlanId)
            .HasColumnName("plan_id")
            .IsRequired();
        builder.Property(x => x.AccountType)
            .HasColumnName("account_type")
            .IsRequired();
        builder.Property(x => x.AccountStatus)
           .HasColumnName("account_status")
           .IsRequired();
        builder.Property(x => x.AccountUpdatedAt)
            .HasColumnName("account_updated_at")
            .IsRequired();
        builder.Property(x => x.PersonId)
            .HasColumnName("person_id")
            .IsRequired();
        builder.Property(x => x.PersonName)
            .HasColumnName("person_name")
            .IsRequired();
        builder.Property(x => x.PersonTradeName)
            .HasColumnName("person_trade_name")
            .IsRequired(false);
        builder.Property(x => x.PersonDocument)
            .HasColumnName("person_document")
            .IsRequired();
        builder.Property(x => x.PersonType)
            .HasColumnName("person_type")
            .IsRequired();
        builder.Property(x => x.PersonStatus)
            .HasColumnName("person_status")
            .IsRequired();
        builder.Property(x => x.PersonUpdatedAt)
            .HasColumnName("person_updated_at")
            .IsRequired();

        //account_number,
        //plan_id,
        //"account_type",
        //account_status,
        //account_updated_at,
        //person_id,
        //person_name,
        //person_trade_name,
        //person_document,
        //"person_type",
        //person_status,
        //person_updated_at,
    }
}