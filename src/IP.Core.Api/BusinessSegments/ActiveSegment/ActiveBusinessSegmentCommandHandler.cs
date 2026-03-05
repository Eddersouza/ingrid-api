namespace IP.Core.Api.BusinessSegments.ActiveSegment;

internal sealed class ActiveBusinessSegmentCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<ActiveBusinessSegmentCommand, ActiveBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<ActiveBusinessSegmentResponse>> Handle(
        ActiveBusinessSegmentCommand command,
        CancellationToken cancellationToken)
    {
        BusinessSegmentId businessSegmentId = BusinessSegmentId.Create(command.Id);

        BusinessSegment? businessSegment = await _businessSegmentRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == businessSegmentId,
            cancellationToken);

        if (businessSegment is null) return BusinessSegmentErrors.BusinessSegmentNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && businessSegment.ActivableInfo.Active)
            return BusinessSegmentErrors.AlreadyActiveStatus(isActived);

        if (isActived) businessSegment.ActivableInfo.SetAsActive();
        else businessSegment.ActivableInfo.SetAsDeactive(reason);

        _businessSegmentRepository.Update(businessSegment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveBusinessSegmentResponse(
            businessSegment.Id.Value!,
            businessSegment.SegmentName.Value!,
            businessSegment.ActivableInfo.Active,
            $"Registro de Segmento de Negócio {actionText} com sucesso!");

        return Result.Success(response);
    }
}