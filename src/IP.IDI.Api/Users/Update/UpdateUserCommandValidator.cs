namespace IP.IDI.Api.Users.Update;

internal class UpdateUserCommandValidator :
    AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        string fieldRoleLabel = "Perfil";

        RuleFor(x => x.Request.RoleId)
           .Cascade(CascadeMode.Stop)
           .NotEmpty()
           .WithMessage(ValidationMessage.RequiredField(fieldRoleLabel));
    }
}