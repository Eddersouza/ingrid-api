namespace IP.Core.Api.Cities.GetCity;

internal sealed class GetCityQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetCityQuery, GetCityResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<GetCityResponse>> Handle(
        GetCityQuery query,
        CancellationToken cancellationToken)
    {
        CityId id = new(query.Id);
        City? currentCity =
            await _cityRepository.Data()
            .Include(s => s.State.Name)
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentCity is null) return CityErrors.CityNotFound;

        GetCityResponse response = new(
            currentCity.Id.Value!,
            currentCity.Name.Value!,
            currentCity.IBGECode!,
            currentCity.StateId.Value!,
            currentCity.State.Name.Value!,
            currentCity.ActivableInfo.Active);

        return Result.Success(response);
    }
}
