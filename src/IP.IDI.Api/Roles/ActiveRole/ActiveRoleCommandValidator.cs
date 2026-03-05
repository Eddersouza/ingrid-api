namespace IP.IDI.Api.Roles.ActiveRole;

internal sealed class ActiveRoleCommandValidator :
    AbstractValidator<ActiveRoleCommand>
{
    public ActiveRoleCommandValidator()
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