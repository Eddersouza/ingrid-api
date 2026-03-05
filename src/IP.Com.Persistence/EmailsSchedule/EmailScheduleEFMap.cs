namespace IP.Com.Persistence.EmailsSchedule;

internal class EmailScheduleEFMap : IEntityTypeConfiguration<EmailSchedule>
{
    public void Configure(EntityTypeBuilder<EmailSchedule> builder)
    {
        builder.ToTable(DBConstants.EmailSchedule);

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Sender)
            .HasColumnType("longtext")
            .IsRequired();
        builder.Property(e => e.Recipient)
            .HasColumnType("longtext")
            .IsRequired();
        builder.Property(e => e.Copy)
            .HasColumnType("longtext")
            .IsRequired(false);
        builder.Property(e => e.CarbonCopy)
            .HasColumnType("longtext")
            .IsRequired(false);
        builder.Property(e => e.Subject)
            .HasMaxLength(EmailSchedule.SUBJECT_MAX_LENGTH)
            .IsRequired();
        builder.Property(e => e.ErrorMessages)
            .HasColumnType("longtext")
            .IsRequired(false);
        builder.Property(e => e.Body)
            .HasColumnType("longtext")
            .IsRequired();
        builder.Property(e => e.Attempts)
            .IsRequired();
        builder.Property(e => e.Sended)
            .IsRequired();
        builder.Property(e => e.LastAttemptDate)
            .IsRequired(false);

        builder.AddBaseEntityFields();
    }
}