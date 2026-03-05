namespace IP.Core.Api.States.UpdateState;

internal class UpdateStateCommandValidator :
    AbstractValidator<UpdateStateCommand>
{
    public UpdateStateCommandValidator()
    {
        string fieldIbgeLabel = "Código IBGE";
        int ibgeMaxLength = State.IBGE_CODE_MAX_LENGTH;
        int ibgeMinLength = State.IBGE_CODE_MIN_LENGTH;

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

        string fieldCodeLabel = "Código UF";
        int codeMaxLength = State.CODE_MAX_LENGTH;
        int codeMinLength = State.CODE_MIN_LENGTH;

        RuleFor(x => x.Request.Code)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldCodeLabel))
            .MaximumLength(codeMaxLength)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldCodeLabel,
                codeMaxLength))
            .MinimumLength(codeMinLength)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldCodeLabel,
                codeMinLength));

        string fieldNameLabel = "Nome";
        int fieldMaxLength = State.NAME_MAX_LENGTH;
        int fieldMinLength = State.NAME_MIN_LENGTH;


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
