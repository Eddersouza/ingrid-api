namespace IP.Core.Api.BusinessSegments.DeleteSegment;

internal sealed class DeleteBusinessSegmentCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteBusinessSegmentCommand, DeleteBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();
    public async Task<Result<DeleteBusinessSegmentResponse>> Handle(
       DeleteBusinessSegmentCommand command,
       CancellationToken cancellationToken)
    {
        BusinessSegmentId businessSegmentId = BusinessSegmentId.Create(command.Id);

        BusinessSegment? businessSegment = await _businessSegmentRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == businessSegmentId,
            cancellationToken);

        if (businessSegment is null) return BusinessSegmentErrors.BusinessSegmentNotFound;

        businessSegment.DeletableInfo.SetReason(command.Request.Reason);

        _businessSegmentRepository.Delete(businessSegment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteBusinessSegmentResponse(
            $"Registro de Segmento de Negócio [{businessSegment.SegmentName.Value}] excluído com sucesso!");

        return Result.Success(response);
    }

}
