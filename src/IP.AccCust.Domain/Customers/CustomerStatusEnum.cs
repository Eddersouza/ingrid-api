namespace IP.AccCust.Domain.Customers;

[JsonConverter(typeof(JsonStringEnumConverter<CustomerStatusEnum>))]
public enum CustomerStatusEnum
{
    [Description("Ativo")] Active = 9,
    [Description("Inativo")] Inactive = 10,
    [Description("Onboarding")] Onboarding = 11
}