using IP.Core.Domain.VO;

namespace IP.Core.Persistence.Employees;

internal class EmployeeEFMap : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable(DBConstants.EmployeeTable);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<EmployeeId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.OwnsOne(entity =>
            entity.Name, entity =>
            {
                entity.Property(prop => prop.Value)
                     .HasMaxLength(Employee.NAME_MAX_LENGTH)
                     .IsRequired();

                entity.Property(prop => prop.ValueNormalized)
                     .HasMaxLength(Employee.NAME_MAX_LENGTH)
                     .IsRequired();
            });

        builder.OwnsOne(entity =>
             entity.CPF, entity =>
             {
                 entity.Property(prop => prop.Value)
                    .HasMaxLength(CPF.LENGTH)
                    .IsRequired(true);
                 entity.Ignore(prop => prop.ValueFormated);
             });

        builder.OwnsOne(entity =>
           entity.User, entity =>
           {
               entity.Property(prop => prop.Id)
                    .IsRequired(false);

               entity.Property(prop => prop.Name)
                     .HasMaxLength(Employee.USERNAME_MAX_LENGTH)
                     .IsRequired(false);

               entity.Property(prop => prop.NameNormalized)
                     .HasMaxLength(Employee.USERNAME_MAX_LENGTH)
                     .IsRequired(false);
           });

        builder.OwnsOne(entity =>
            entity.Manager, entity =>
            {
                entity.Property(prop => prop.Id)
                     .IsRequired(false);

                entity.Property(prop => prop.Name)
                      .HasMaxLength(Employee.USERNAME_MAX_LENGTH)
                      .IsRequired(false);

                entity.Property(prop => prop.NameNormalized)
                      .HasMaxLength(Employee.USERNAME_MAX_LENGTH)
                      .IsRequired(false);
            });

        builder.AddBaseEntityFields();
    }
}