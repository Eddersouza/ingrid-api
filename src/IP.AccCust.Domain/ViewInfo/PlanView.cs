namespace IP.AccCust.Domain.ViewInfo;

public sealed class PlanView
{
    public int PlanId { get; private set; }
    public string PlanName { get; private set; } = string.Empty;
    public string PlanStatus { get; private set; } = string.Empty;
    public DateTimeOffset UpdateDate { get; private set; }
}
