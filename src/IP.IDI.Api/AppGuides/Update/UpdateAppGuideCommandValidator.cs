namespace IP.IDI.Api.AppGuides.Update;

internal sealed class UpdateAppGuideCommandValidator :
    AbstractValidator<UpdateAppGuideCommand>
{
    public UpdateAppGuideCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = AppGuide.NAME_MAX_LENGTH;
        int fieldMinLength = AppGuide.NAME_MIN_LENGTH;

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

        string fieldLinkIdLabel = "LinkId";
        int fieldLinkIdMaxLength = AppGuide.LINK_ID_MAX_LENGTH;
        int fieldLinkIdMinLength = AppGuide.LINK_ID_MIN_LENGTH;

        RuleFor(x => x.Request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldLinkIdLabel))
            .MaximumLength(fieldMaxLength)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldLinkIdLabel,
                fieldLinkIdMaxLength))
            .MinimumLength(fieldMinLength)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldLinkIdLabel,
                fieldLinkIdMinLength));
    }
}