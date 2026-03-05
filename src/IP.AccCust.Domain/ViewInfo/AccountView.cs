namespace IP.AccCust.Domain.ViewInfo;

public sealed class AccountView
{
    public int AccountNumber { get; set; }
    public int PlanId { get; set; }
    public string AccountType { get; set; } = null!;
    public string AccountStatus { get; set; } = null!;
    public DateTimeOffset AccountUpdatedAt { get; set; }
    public int PersonId { get; set; }
    public string PersonName { get; set; } = null!;
    public string PersonTradeName { get; set; } = null!;
    public string PersonDocument { get; set; } = null!;
    public string PersonType { get; set; } = null!;
    public string PersonStatus { get; set; } = null!;
    public DateTimeOffset PersonUpdatedAt { get; set; }
}