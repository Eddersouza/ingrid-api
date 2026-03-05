namespace IP.Core.Api.BusinessBranches.DeleteBusinessBranch;

internal sealed class DeleteBusinessBranchCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteBusinessBranchCommand, DeleteBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();
    public async Task<Result<DeleteBusinessBranchResponse>> Handle(
       DeleteBusinessBranchCommand command,
       CancellationToken cancellationToken)
    {
        BusinessBranchId businessBranchId = BusinessBranchId.Create(command.Id);

        BusinessBranch? businessBranch = await _businessBranchRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == businessBranchId,
            cancellationToken);

        if (businessBranch is null) return BusinessBranchErrors.BusinessBranchNotFound;

        businessBranch.DeletableInfo.SetReason(command.Request.Reason);

        _businessBranchRepository.Delete(businessBranch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteBusinessBranchResponse(
            $"Registro de Ramo de Negócio [{businessBranch.Name.Value}] excluído com sucesso!");

        return Result.Success(response);
    }

}

