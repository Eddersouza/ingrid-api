using IP.Shared.Domain.Entities;

namespace IP.Shared.Persistence;

public static class EntityTypeBuilderExtension
{
    public static void AddBaseEntityFields<TEntity>(
           this EntityTypeBuilder<TEntity> builder,
           bool defaultValueToActive = true,
           bool defaultValueToDelete = false)
           where TEntity : class
    {
        builder.AddDomainEventsEntityConfig();
        builder.AddAuditableEntityFields();
        builder.AddActivableEntityFields(defaultValueToActive);
        builder.AddDeletableEntityFields(defaultValueToDelete);
    }

    private static void AddActivableEntityFields<TEntity>(
           this EntityTypeBuilder<TEntity> builder,
           bool defaultValueToActive) where TEntity : class
    {
        var entityIsActivable =
            typeof(IEntityActivable).IsAssignableFrom(typeof(TEntity));

        if (!entityIsActivable) return;

        builder.OwnsOne(entity =>
            ((IEntityActivable)entity).ActivableInfo, entity =>
            {
                entity.Property(prop => prop.Active)
                        .HasDefaultValue(defaultValueToActive)
                        .IsRequired();
                entity.Property(prop => prop.InativeReason)
                        .HasColumnType($"text({IEntityActivable.REASON_MAX_LENGTH})")
                        .IsRequired(false);
            });
    }

    private static void AddAuditableEntityFields<TEntity>(
               this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        var entityIsAuditable =
            typeof(IEntityAuditable).IsAssignableFrom(typeof(TEntity));

        if (!entityIsAuditable) return;

        builder.OwnsOne(entity => ((IEntityAuditable)entity).AuditableInfo, entity =>
        {
            entity.Property(prop => prop.CreatedBy)
                .HasMaxLength(IEntityAuditable.USER_MAX_LENGTH)
                .IsRequired();
            entity.Property(prop => prop.CreatedDate)
                .IsRequired();
            entity.Property(prop => prop.UpdatedBy)
                .HasMaxLength(IEntityAuditable.USER_MAX_LENGTH)
                .IsRequired(false);
            entity.Property(prop => prop.UpdatedDate)
                .IsRequired(false);
        });
    }

    private static void AddDeletableEntityFields<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
          bool defaultValueToActive) where TEntity : class
    {
        var entityIsDeletable =
            typeof(IEntityDeletable).IsAssignableFrom(typeof(TEntity));

        if (!entityIsDeletable) return;

        builder.OwnsOne(entity =>
            ((IEntityDeletable)entity).DeletableInfo, entity =>
            {
                entity.Property(prop => prop.Deleted)
                    .IsRequired()
                    .HasDefaultValue(defaultValueToActive);
                entity.Property(prop => prop.DeletedReason)
                    .HasColumnType($"text({IEntityDeletable.REASON_MAX_LENGTH})")
                    .IsRequired(false);
            });

        builder.HasQueryFilter(deleteEntity =>
            !((IEntityDeletable)deleteEntity).DeletableInfo.Deleted);
    }

    private static void AddDomainEventsEntityConfig<TEntity>(
                       this EntityTypeBuilder<TEntity> builder) where TEntity : class
    {
        bool entityHasDomainEvents =
           typeof(IEntityDomainEvents).IsAssignableFrom(typeof(TEntity));

        if (!entityHasDomainEvents) return;

        _ = builder.Ignore(x => (x as IEntityDomainEvents)!.EventsInfo);
    }
}