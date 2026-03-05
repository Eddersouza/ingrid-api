namespace IP.Com.Domain.EmailsSchedule;

public class EmailSchedule : EntityAuditable<Guid>
{
    public const int BODY_MAX_LENGTH = int.MaxValue;
    public const int CARBON_COPY_MAX_LENGTH = int.MaxValue;
    public const int COPY_MAX_LENGTH = int.MaxValue;
    public const int RECIPIENT_MAX_LENGTH = int.MaxValue;
    public const int SENDER_MAX_LENGTH = int.MaxValue;
    public const int SUBJECT_MAX_LENGTH = 256;
    public const int MAX_ATTEMPTS = 3;

    public EmailSchedule(
        string sender,
        string recipient,
        string subject,
        string body) : this()
    {
        Sender = sender;
        Recipient = recipient;
        Subject = subject;
        Body = body;
        Sended = false;
        Attempts = 0;
    }

    protected EmailSchedule()
    {
        Id = Guid.CreateVersion7();
    }

    public int Attempts { get; private set; }

    public string Body { get; private set; } = string.Empty;

    public string? CarbonCopy { get; private set; } = null;

    public string Copy { get; private set; } = string.Empty;

    public string ErrorMessages { get; private set; } = string.Empty;
    public string Recipient { get; private set; } = string.Empty;

    public bool Sended { get; private set; }

    public string Sender { get; private set; } = string.Empty;

    public string Subject { get; private set; } = string.Empty;

    public DateTime? LastAttemptDate { get; private set; } = null;

    public static EmailSchedule Create(
        string sender,
        string recipient,
        string subject,
        string body) => new(sender, recipient, subject, body);

    public void SetErrorMessages(string messages) => ErrorMessages += messages;

    public void NewAttempt()
    {
        Attempts++;
        LastAttemptDate = DateTime.UtcNow;
    }

    public void SetSended() => Sended = true;

    public override string ToEntityName() => "Email Agendado";
}