namespace IP.AccCust.Domain.AccountsIP;

[JsonConverter(typeof(JsonStringEnumConverter<AccountIPStatus>))]
public enum AccountIPStatus
{
    [Description("Ativa")] Active = 12,
    [Description("Inativa")] Inactive = 13,
    [Description("Bloqueada")] Blocked = 14,
    [Description("Fechada")] Closed = 15
}