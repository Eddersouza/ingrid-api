namespace IP.AccCust.Persistence.PlansView;

internal sealed class PlanViewEFMap : IEntityTypeConfiguration<PlanView>
{
    public void Configure(EntityTypeBuilder<PlanView> builder)
    {
        builder.HasNoKey();
        builder.ToView(DBConstants.PlanView, DBConstants.Schema);
        builder.Property(x => x.PlanId)
            .HasColumnName("plan_id")
            .IsRequired();
        builder.Property(x => x.PlanName)
            .HasColumnName("plan_name")
            .IsRequired();
        builder.Property(x => x.PlanStatus)
            .HasColumnName("plan_status")
            .IsRequired();
        builder.Property(x => x.UpdateDate)
           .HasColumnName("plan_updated_at")
           .IsRequired();
    }
}
