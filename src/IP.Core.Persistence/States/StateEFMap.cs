namespace IP.Core.Persistence.States;

internal class StateEFMap : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable(DBConstants.StateTable);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(new EntityIdValueConverter<StateId>())
            .ValueGeneratedNever()
            .IsRequired(true);

        builder.Property(s => s.Code)
            .HasMaxLength(State.CODE_MAX_LENGTH)
            .IsRequired(true);

        builder.Property(s => s.IBGECode)
          .HasMaxLength(State.IBGE_CODE_MAX_LENGTH)
          .IsRequired(true);

        builder.OwnsOne(entity =>
              entity.Name, entity =>
              {
                  entity.Property(prop => prop.Value)
                       .HasMaxLength(State.NAME_MAX_LENGTH)
                       .IsRequired();

                  entity.Property(prop => prop.ValueNormalized)
                       .HasMaxLength(State.NAME_MAX_LENGTH)
                       .IsRequired();
              });

        builder.AddBaseEntityFields();
    }
}