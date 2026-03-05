namespace IP.Core.Api.BusinessSegments.UpdateSegment;

internal sealed class UpdateBusinessSegmentCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateBusinessSegmentCommand, UpdateBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<UpdateBusinessSegmentResponse>> Handle(
        UpdateBusinessSegmentCommand command,
        CancellationToken cancellationToken)
    {
        BusinessSegmentId businessSegmentId = BusinessSegmentId.Create(command.Id);

        BusinessSegment? businessSegment = await _businessSegmentRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == businessSegmentId,
            cancellationToken);

        if (businessSegment is null) return BusinessSegmentErrors.BusinessSegmentNotFound;

        businessSegment.Update(
             command.Request.SegmentName
             );

        _businessSegmentRepository.Update(businessSegment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var response = new UpdateBusinessSegmentResponse(
            businessSegment.Id.Value!,
            businessSegment.SegmentName.Value!,
            $"Registro de Segmento de Negócio [{businessSegment.SegmentName.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}

