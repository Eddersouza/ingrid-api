namespace IP.Core.Api.Cities.CreateCity;

internal class CreateCityCommandValidator :
    AbstractValidator<CreateCityCommand>
{
    public CreateCityCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = City.NAME_MAX_LENGTH;
        int fieldMinLength = City.NAME_MIN_LENGTH;
        RuleFor(x => x.Request.Name)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
             .MaximumLength(fieldMaxLength)
             .WithMessage(ValidationMessage.RequiredMaxLengthField(
                 fieldNameLabel,
                 fieldMaxLength))
             .MinimumLength(fieldMinLength)
             .WithMessage(ValidationMessage.RequiredMinLengthField(
                 fieldNameLabel,
                 fieldMinLength));

        string fieldIbgeLabel = "Código IBGE";
        int ibgeMaxLength = City.IBGE_CODE_MAX_LENGTH;
        int ibgeMinLength = City.IBGE_CODE_MIN_LENGTH;
        RuleFor(x => x.Request.IBGECode)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldIbgeLabel))
             .MaximumLength(ibgeMaxLength)
             .WithMessage(ValidationMessage.RequiredMaxLengthField(
                 fieldIbgeLabel,
                 ibgeMaxLength))
             .MinimumLength(ibgeMinLength)
             .WithMessage(ValidationMessage.RequiredMinLengthField(
                 fieldIbgeLabel,
                 ibgeMinLength));

        string fieldStateIdLabel = "Estado";
        RuleFor(x => x.Request.StateId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldStateIdLabel));
    }
}
