namespace IP.Core.Api.Customers.Create;

internal sealed class CreateCustomerCommandValidator :
    AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        string fieldNameLabel = "Nome";
        string fieldTradingNameLabel = "Nome Fantasia";
        string fieldCPFLabel = "CPF/CNPJ";

        RuleFor(x => x.Request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
            .MaximumLength(Customer.NAME_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldNameLabel,
                Customer.NAME_MAX_LENGTH))
            .MinimumLength(Customer.NAME_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldNameLabel,
                Customer.NAME_MIN_LENGTH));

        RuleFor(x => x.Request.TradingName)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(Customer.TRADINGNAME_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldTradingNameLabel,
                Customer.TRADINGNAME_MAX_LENGTH))
            .MinimumLength(Customer.TRADINGNAME_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldTradingNameLabel,
                Customer.TRADINGNAME_MIN_LENGTH))
            .When(field => field.Request.TradingName.IsNotNullOrWhiteSpace());

        RuleFor(x => x.Request.DocumentNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldCPFLabel));

        RuleFor(x => x.Request.PersonTypeCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField("Tipo"));

        RuleFor(x => x.Request.StatusCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField("Status"));
    }
}