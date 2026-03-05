namespace IP.AccIPInfo.Api.AccountSubscriptions.UpdateAccountSubscription;

internal class UpdateAccountSubscriptionCommandValidator :
    AbstractValidator<UpdateAccountSubscriptionCommand>
{
    public UpdateAccountSubscriptionCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = AccountSubscription.NAME_MAX_LENGTH;
        int fieldMinLength = AccountSubscription.NAME_MIN_LENGTH;
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