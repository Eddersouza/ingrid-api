namespace IP.Core.Api.BusinessSegments.Create;

internal class CreateBusinessSegmentCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<CreateBusinessSegmentCommand, CreateBusinessSegmentResponse>
{
    private readonly IBusinessSegmentRepository _businessSegmentRepository =
        _unitOfWork.GetRepository<IBusinessSegmentRepository>();

    public async Task<Result<CreateBusinessSegmentResponse>> Handle(
        CreateBusinessSegmentCommand command,
        CancellationToken cancellationToken)
    {
        BusinessSegment? businessSegment = await _businessSegmentRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.SegmentName.Value == command.Request.SegmentName && 
                s.BusinessBranchId == BusinessBranchId.Create(command.Request.BusinessBranchId),
                cancellationToken);

        if (businessSegment is not null)
            return BusinessSegmentErrors.BusinessSegmentAlreadyExists;

        businessSegment = BusinessSegment.Create(
            BusinessBranchId.Create(command.Request.BusinessBranchId),
            command.Request.SegmentName
            );

        await _businessSegmentRepository.Create(businessSegment);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateBusinessSegmentResponse(
            businessSegment.Id.Value,
            BusinessBranchId.Create(command.Request.BusinessBranchId),
            businessSegment.SegmentName.Value,
            $"Registro de Segmento de Negócio [{businessSegment.SegmentName.Value}] criado com sucesso!",
            businessSegment.ActivableInfo.Active);

        return await Task.FromResult(Result.Success(response));
    }
}
