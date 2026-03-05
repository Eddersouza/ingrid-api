namespace IP.IDI.Api.Roles.Update;

internal sealed class UpdateRoleCommandValidator : 
    AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = AppRole.NAME_MAX_LENGTH;
        int fieldMinLength = AppRole.NAME_MIN_LENGTH;

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
    }
}