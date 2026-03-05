using IP.Core.Domain.BusinessBranches;

namespace IP.Core.Persistence.BusinessBranches;

internal class BusinessBranchEFMap : IEntityTypeConfiguration<BusinessBranch>
{
    public void Configure(EntityTypeBuilder<BusinessBranch> builder)
    {
        builder.ToTable(DBConstants.BusinessBranchTable);

        builder.HasKey(x => x.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<BusinessBranchId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(BusinessBranch.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(BusinessBranch.NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.AddBaseEntityFields();
    }
}

