namespace IP.AccIPInfo.Api.IntegratorSystems.GetSystem;

internal sealed class GetSystemQueryHandler(
    IAccIPUoW _unitOfWork) :
    IQueryHandler<GetSystemQuery, GetSystemResponse>
{
    private readonly IIntegratorSystemRepository _integratorSystemRepository =
        _unitOfWork.GetRepository<IIntegratorSystemRepository>();

    public async Task<Result<GetSystemResponse>> Handle(
        GetSystemQuery query,
        CancellationToken cancellationToken)
    {
        IntegratorSystemId id = new(query.Id);
        IntegratorSystem? currentSystem =
            await _integratorSystemRepository.Data()
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentSystem is null) return IntegratorSystemErrors.IntegratorSystemNotFound;

        GetSystemResponse response = new(
            currentSystem.Id.Value!,
            currentSystem.Name.Value!,
            currentSystem.SiteUrl!,
            currentSystem.Description!,
            currentSystem.ActivableInfo.Active);

        return Result.Success(response);
    }
}