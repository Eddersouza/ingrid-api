namespace IP.Core.Api.Customers;

public class CustomerResponseData(Customer customer)
{
    public Guid Id { get; set; } = customer.Id.Value;

    public string Name { get; set; } = customer.Name.Value;

    public string? TradingName { get; set; } = customer.TradingName.Value;

    public string DocumentNumber { get; set; } = customer.DocumentNumber.ValueFormated;   

    public PersonTypeEnum PersonTypeCode { get; set; } = customer.PersonTypeCode;
    public string PersonTypeDescription => PersonTypeCode.GetDescription();
    public CustomerStatusEnum StatusCode { get; set; } = customer.StatusCode;
    public string StatusDescription => StatusCode.GetDescription();
}