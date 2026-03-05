namespace IP.Core.Api.Employees.Create;

internal sealed class CreateEmployeeCommandValidator :
    AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        string fieldNameLabel = "Nome";
        string fieldCPFLabel = "CPF";

        RuleFor(x => x.Request.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldNameLabel))
            .MaximumLength(Employee.NAME_MAX_LENGTH)
            .WithMessage(ValidationMessage.RequiredMaxLengthField(
                fieldNameLabel,
                Employee.NAME_MAX_LENGTH))
            .MinimumLength(Employee.NAME_MIN_LENGTH)
            .WithMessage(ValidationMessage.RequiredMinLengthField(
                fieldNameLabel,
                Employee.NAME_MIN_LENGTH));

        RuleFor(x => x.Request.CPF)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationMessage.RequiredField(fieldCPFLabel));
    }
}