namespace IP.Core.Api.BusinessBranches.Create;

internal class CreateBusinessBranchCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<CreateBusinessBranchCommand, CreateBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();

    public async Task<Result<CreateBusinessBranchResponse>> Handle(
        CreateBusinessBranchCommand command,
        CancellationToken cancellationToken)
    {
        BusinessBranch? businessBranch = await _businessBranchRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name,
                cancellationToken);

        if (businessBranch is not null)
            return BusinessBranchErrors.BusinessBranchAlreadyExists;

        businessBranch = BusinessBranch.Create(
            command.Request.Name
            );

        await _businessBranchRepository.Create(businessBranch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateBusinessBranchResponse(
            businessBranch.Id.Value,
            businessBranch.Name.Value!,
            $"Registro de Ramo de Negócio [{businessBranch.Name.Value}] criado com sucesso!",
            businessBranch.ActivableInfo.Active);

        return await Task.FromResult(Result.Success(response));
    }
}
