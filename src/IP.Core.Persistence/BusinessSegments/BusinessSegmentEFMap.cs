using IP.Core.Domain.BusinessSegments;

namespace IP.Core.Persistence.BusinessSegments;

internal class BusinessSegmentEFMap : IEntityTypeConfiguration<BusinessSegment>
{
    public void Configure(EntityTypeBuilder<BusinessSegment> builder)
    {
        builder.ToTable(DBConstants.BusinessSegmentTable);

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(new EntityIdValueConverter<BusinessSegmentId>())
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.BusinessBranch)
            .WithMany(x => x.Segments)
            .HasForeignKey(x => x.BusinessBranchId)
            .IsRequired(true);


        builder.OwnsOne(entity =>
              entity.SegmentName, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(BusinessSegment.SEGMENT_NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(BusinessSegment.SEGMENT_NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.AddBaseEntityFields();
    }
}
