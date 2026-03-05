namespace IP.AccIPInfo.Api.IntegratorSystems.UpdateSystem;

internal class UpdateSystemCommandValidator : AbstractValidator<UpdateSystemCommand>
{
    public UpdateSystemCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = IntegratorSystem.NAME_MAX_LENGTH;
        int fieldMinLength = IntegratorSystem.NAME_MIN_LENGTH;
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

        string fieldSiteUrlLabel = "Site";
        int siteUrlMaxLength = IntegratorSystem.SITE_URL_MAX_LENGTH;
        RuleFor(x => x.Request.SiteUrl)
             .Cascade(CascadeMode.Stop)
             .MaximumLength(siteUrlMaxLength)
             .WithMessage(ValidationMessage.RequiredMaxLengthField(
                 fieldSiteUrlLabel,
                 siteUrlMaxLength));

        string fieldDescriptionLabel = "Descrição";
        int descriptionMaxLength = IntegratorSystem.DESCRIPTION_MAX_LENGTH;
        RuleFor(x => x.Request.Description)
             .Cascade(CascadeMode.Stop)
             .MaximumLength(descriptionMaxLength)
             .WithMessage(ValidationMessage.RequiredMaxLengthField(
                 fieldDescriptionLabel,
                 descriptionMaxLength));
    }
}