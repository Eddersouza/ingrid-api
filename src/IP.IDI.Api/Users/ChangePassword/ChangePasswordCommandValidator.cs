namespace IP.IDI.Api.Users.ChangePassword;

internal class ChangePasswordCommandValidator :
    AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        const string fieldTokenLabel = "Token";
        const string fieldPasswordLabel = "Senha";
        const string fieldPasswordConfirmationLabel = "Confirmação de Senha";

        RuleFor(x => x.Request.Token)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldTokenLabel));

        RuleFor(x => x.Request.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldPasswordLabel))
            .Equal(x => x.Request.ConfirmPassword)
            .WithMessage(ValidationMessage.FieldAMustBeEqualFieldB(fieldPasswordLabel, fieldPasswordConfirmationLabel));

        RuleFor(x => x.Request.ConfirmPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldPasswordConfirmationLabel));
    }
}