namespace IP.Core.Api.Customers.Update;

public sealed record UpdateCustomerCommand(Guid Id, UpdateCustomerRequest Request) :
    ICommand<UpdateCustomerResponse>, ILoggableData;

public sealed class UpdateCustomerRequest
{
    [Required]
    [MinLength(Customer.NAME_MIN_LENGTH)]
    [MaxLength(Customer.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [MinLength(Customer.TRADINGNAME_MIN_LENGTH)]
    [MaxLength(Customer.TRADINGNAME_MAX_LENGTH)]
    public string TradingName { get; set; } = string.Empty;

    [Required]
    [Description("Número de CPF/CNPJ em formato XXX.XXX.XXX-XX ou XXXXXXXXXXXX(CPF) / XX.XXX.XXX/XXXXX-XX ou XXXXXXXXXXXXXX(CNPJ)")]
    public string DocumentNumber { get; set; } = string.Empty;

    [Required]
    public PersonTypeEnum? PersonTypeCode { get; set; } = null;

    [Required]
    public CustomerStatusEnum? StatusCode { get; set; } = null;
}

public sealed class UpdateCustomerResponse(
    Customer Customer,
    string message) :
    ResolvedData<CustomerResponseData>(new CustomerResponseData(Customer), message);