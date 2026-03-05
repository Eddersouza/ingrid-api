namespace IP.Shared.Persistence.Data;

public class AuditTrailEFMap : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        string maxTextTypeName = "longtext";
        builder.ToTable("AuditTrails");

        builder.HasKey(b => b.Id);

        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.RequestId).IsRequired();
        builder.Property(p => p.UserName)
            .HasMaxLength(256)
            .IsRequired();
        builder.Property(p => p.Type)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.TableName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(p => p.PrimaryKey)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.AffectedColumns)
            .HasColumnType(maxTextTypeName)
            .IsRequired(false);
        builder.Property(p => p.OldValues)
            .HasColumnType(maxTextTypeName)
            .IsRequired(false);
        builder.Property(p => p.NewValues)
            .HasColumnType(maxTextTypeName)
            .IsRequired(false);
    }
}