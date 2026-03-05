namespace IP.IDI.Api.Roles.DeleteRole;

internal sealed class DeleteRoleCommandValidator :
    AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        string fieldNameLabel = "Motivo";
        int fieldMaxLength = IEntityDeletable.REASON_MAX_LENGTH;
        int fieldMinLength = IEntityDeletable.REASON_MIN_LENGTH;

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
                fieldMinLength));
    }
}