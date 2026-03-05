namespace IP.AccIPInfo.Api.AccountSubscriptions.DeleteAccountSubscription;

internal sealed class DeleteAccountSubscriptionCommandValidator :
    AbstractValidator<DeleteAccountSubscriptionCommand>
{
    public DeleteAccountSubscriptionCommandValidator()
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