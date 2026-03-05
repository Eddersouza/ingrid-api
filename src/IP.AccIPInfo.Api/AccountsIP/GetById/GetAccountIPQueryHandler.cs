namespace IP.AccIPInfo.Api.AccountsIP.GetById;

internal sealed class GetAccountIPQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetAccountIPQuery, GetAccountIPResponse>
{
    private readonly IAccountIPRepository _accountIPRepository =
        _unitOfWork.GetRepository<IAccountIPRepository>();

    public async Task<Result<GetAccountIPResponse>> Handle(
        GetAccountIPQuery query,
        CancellationToken cancellationToken)
    {
        AccountIPId accountIPId = AccountIPId.Create(query.Id);

        AccountIP? currentRecord = await _accountIPRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(AccountIP =>
                AccountIP.Id == accountIPId,
                cancellationToken);

        if (currentRecord is null) return AccountIPErrors.NotFound;

        return Result.Success(new GetAccountIPResponse(currentRecord!));
    }
}