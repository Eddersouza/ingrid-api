namespace IP.IDI.Api.Users.Authenticate;

internal class AuthenticateUserCommandValidator :
    AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        string fieldUserOREmailLabel = "Usuário ou Email";
        string fieldPasswordLabel = "Senha";

        RuleFor(x => x.Request.UserOrEmail)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldUserOREmailLabel))
            .MaximumLength(AppUser.USERNAME_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldUserOREmailLabel,
                AppUser.USERNAME_MAX_LENGTH))
            .MinimumLength(AppUser.USERNAME_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldUserOREmailLabel,
                AppUser.USERNAME_MIN_LENGTH));

        RuleFor(x => x.Request.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldPasswordLabel))
            .MaximumLength(AppUser.PASSWORD_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldPasswordLabel,
                AppUser.PASSWORD_MAX_LENGTH))
            .MinimumLength(AppUser.PASSWORD_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldPasswordLabel,
                AppUser.PASSWORD_MIN_LENGTH));
    }
}