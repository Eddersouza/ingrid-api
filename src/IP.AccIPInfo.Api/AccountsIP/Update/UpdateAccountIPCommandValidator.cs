namespace IP.AccIPInfo.Api.AccountsIP.Update;

internal sealed class UpdateAccountIPCommandValidator :
    AbstractValidator<UpdateAccountIPCommand>
{
    public UpdateAccountIPCommandValidator()
    {
        string fieldNameLabel = "Número";

        RuleFor(x => x.Request.Number)
              .Cascade(CascadeMode.Stop)
              .NotEqual(0).WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
              .WithMessage(ValidationMessage.RequiredField(
                  fieldNameLabel));

        RuleFor(x => x.Request.Alias)
           .Cascade(CascadeMode.Stop)
           .MaximumLength(AccountIPAlias.MAX_LENGTH)
           .WithMessage(ValidationMessage.RequiredMaxLengthField(
               "Apelido",
               AccountIPAlias.MAX_LENGTH))
           .When(x => x.Request.Alias.IsNotNullOrWhiteSpace());

        RuleFor(x => x.Request.Customer)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField("Cliente"));

        RuleFor(x => x.Request.StatusCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField("Status"));

        RuleFor(x => x.Request.TypeCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField("Tipo"));
    }
}