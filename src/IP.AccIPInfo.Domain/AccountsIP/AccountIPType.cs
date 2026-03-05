namespace IP.AccIPInfo.Domain.AccountsIP;

[JsonConverter(typeof(JsonStringEnumConverter<AccountIPType>))]
public enum AccountIPType
{
    [Description("Transacional")] Transactional = 16,
    [Description("Gestor de Pagamento")] PaymentManagement = 17,
    [Description("Gestor de Cripto")] CriptoManagement = 18,
    [Description("Gestor de Bet")] BetManagement = 19
}