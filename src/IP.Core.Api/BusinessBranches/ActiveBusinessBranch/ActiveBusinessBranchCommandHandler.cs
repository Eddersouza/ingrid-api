namespace IP.Core.Api.BusinessBranches.ActiveBusinessBranch;

internal sealed class ActiveBusinessBranchCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<ActiveBusinessBranchCommand, ActiveBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();

    public async Task<Result<ActiveBusinessBranchResponse>> Handle(
        ActiveBusinessBranchCommand command,
        CancellationToken cancellationToken)
    {
        BusinessBranchId businessBranchId = BusinessBranchId.Create(command.Id);

        BusinessBranch? businessBranch = await _businessBranchRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == businessBranchId,
            cancellationToken);

        if (businessBranch is null) return BusinessBranchErrors.BusinessBranchNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && businessBranch.ActivableInfo.Active)
            return BusinessBranchErrors.AlreadyActiveStatus(isActived);

        if (isActived) businessBranch.ActivableInfo.SetAsActive();
        else businessBranch.ActivableInfo.SetAsDeactive(reason);

        _businessBranchRepository.Update(businessBranch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveBusinessBranchResponse(
            businessBranch.Id.Value!,
            businessBranch.Name.Value!,
            businessBranch.ActivableInfo.Active,
            $"Registro de Ramo de Negócio {actionText} com sucesso!");

        return Result.Success(response);
    }
}