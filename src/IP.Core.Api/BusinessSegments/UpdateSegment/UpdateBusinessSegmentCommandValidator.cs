namespace IP.Core.Api.BusinessSegments.UpdateSegment;

internal class UpdateBusinessSegmentCommandValidator : AbstractValidator<UpdateBusinessSegmentCommand>
{
    public UpdateBusinessSegmentCommandValidator()
    {
        string fieldNameLabel = "Nome";
        int fieldMaxLength = BusinessSegment.SEGMENT_NAME_MAX_LENGTH;
        int fieldMinLength = BusinessSegment.SEGMENT_NAME_MIN_LENGTH;

        RuleFor(x => x.Request.SegmentName)
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
