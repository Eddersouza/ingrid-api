namespace IP.Core.Api.BusinessBranches.GetBusinessBranch;

internal sealed class GetBusinessBranchQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetBusinessBranchQuery, GetBusinessBranchResponse>
{
    private readonly IBusinessBranchRepository _businessBranchRepository =
        _unitOfWork.GetRepository<IBusinessBranchRepository>();
    public async Task<Result<GetBusinessBranchResponse>> Handle(
        GetBusinessBranchQuery query,
        CancellationToken cancellationToken)
    {
        BusinessBranchId id = BusinessBranchId.Create(query.Id);
        BusinessBranch? currentBranch =
            await _businessBranchRepository.Data()
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentBranch is null) return BusinessBranchErrors.BusinessBranchNotFound;

        GetBusinessBranchResponse response = new(
            currentBranch.Id.Value!,
            currentBranch.Name.Value!,
            currentBranch.ActivableInfo.Active);

        return Result.Success(response);
    }
}
