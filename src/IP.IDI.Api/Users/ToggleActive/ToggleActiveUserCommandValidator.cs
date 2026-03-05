namespace IP.IDI.Api.Users.ToggleActive;

internal sealed class ToggleActiveUserCommandValidator :
    AbstractValidator<ToggleActiveUserCommand>
{
    public ToggleActiveUserCommandValidator()
    {
        string fieldNameLabel = "Motivo";
        int fieldMaxLength = IEntityActivable.REASON_MAX_LENGTH;
        int fieldMinLength = IEntityActivable.REASON_MIN_LENGTH;

        RuleFor(x => x.Request.Reason)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
            .MaximumLength(fieldMaxLength)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldNameLabel,
                fieldMaxLength))
            .MinimumLength(fieldMinLength)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldNameLabel,
                fieldMinLength))
            .When(x => !x.Request.Active);
    }
}