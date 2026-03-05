namespace IP.Core.Api.BusinessBranches.UpdateBusinessBranch;

internal class UpdateBusinessBranchCommandValidator : AbstractValidator<UpdateBusinessBranchCommand>
{
    public UpdateBusinessBranchCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = BusinessBranch.NAME_MAX_LENGTH;
        int fieldMinLength = BusinessBranch.NAME_MIN_LENGTH;
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
