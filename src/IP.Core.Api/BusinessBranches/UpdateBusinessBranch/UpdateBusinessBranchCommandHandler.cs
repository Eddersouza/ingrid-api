namespace IP.Core.Api.BusinessBranches.UpdateBusinessBranch;

internal sealed class UpdateBusinessBranchCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateBusinessBranchCommand, UpdateBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();

    public async Task<Result<UpdateBusinessBranchResponse>> Handle(
        UpdateBusinessBranchCommand command,
        CancellationToken cancellationToken)
    {
        BusinessBranchId businessBranchId = BusinessBranchId.Create(command.Id);

        BusinessBranch? businessBranch = await _businessBranchRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == businessBranchId,
            cancellationToken);

        if (businessBranch is null) return BusinessBranchErrors.BusinessBranchNotFound;

        businessBranch.Update(
             command.Request.Name
             );

        _businessBranchRepository.Update(businessBranch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateBusinessBranchResponse(
            businessBranch.Id.Value!,
            businessBranch.Name.Value!,
            $"Registro de Ramo de Negócio [{businessBranch.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}
