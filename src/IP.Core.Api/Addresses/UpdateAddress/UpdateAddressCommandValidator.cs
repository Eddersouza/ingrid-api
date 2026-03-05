namespace IP.Core.Api.Addresses.UpdateAddress;

internal class UpdateAddressCommandValidator :
    AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = Address.NAME_MAX_LENGTH;
        int fieldMinLength = Address.NAME_MIN_LENGTH;
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

        string fieldNeighborhoodLabel = "Bairro";
        int neighborhoodMaxLength = Address.NEIGHBORHOOD_MAX_LENGTH;
        int neighborhoodMinLength = Address.NEIGHBORHOOD_MIN_LENGTH;
        RuleFor(x => x.Request.Neighborhood)
             .Cascade(CascadeMode.Stop)
             .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNeighborhoodLabel))
             .MaximumLength(fieldMaxLength)
             .WithMessage(ValidationMessage.RequiredMaxLengthField(
                 fieldNeighborhoodLabel,
                 neighborhoodMaxLength))
             .MinimumLength(fieldMinLength)
             .WithMessage(ValidationMessage.RequiredMinLengthField(
                 fieldNeighborhoodLabel,
                 neighborhoodMinLength));

        string fieldIbgeLabel = "CEP";
        int ibgeMaxLength = Address.CODE_MAX_LENGTH;
        int ibgeMinLength = Address.CODE_MIN_LENGTH;
        RuleFor(x => x.Request.Code)
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

        string fieldStateIdLabel = "Cidade";
        RuleFor(x => x.Request.CityId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldStateIdLabel));
    }
}
