namespace IP.IDI.Api.Users.CreateUser;

internal class CreateUserCommandValidator :
    AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        string fieldNameLabel = "Nome";
        string fieldEmailLabel = "E-mail";
        string fieldRoleLabel = "Perfil";

        RuleFor(x => x.Request.User)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
            .MaximumLength(AppUser.USERNAME_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldNameLabel,
                AppUser.USERNAME_MAX_LENGTH))
            .MinimumLength(AppUser.USERNAME_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldNameLabel,
                AppUser.USERNAME_MIN_LENGTH));

        RuleFor(x => x.Request.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldEmailLabel))
            .MaximumLength(AppUser.EMAIL_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(fieldEmailLabel, AppUser.EMAIL_MAX_LENGTH))
            .Matches(@"(?:[a-z0-9!#$%&'*+\x2f=?^_`\x7b-\x7d~\x2d]+(?:\.[a-z0-9!#$%&'*+\x2f=?^_`\x7b-\x7d~\x2d]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9\x2d]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9\x2d]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9\x2d]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])")
            .WithMessage(ValidationMessage.RequiredEmailField(fieldEmailLabel));

        RuleFor(x => x.Request.RoleId)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldRoleLabel));
    }
}
