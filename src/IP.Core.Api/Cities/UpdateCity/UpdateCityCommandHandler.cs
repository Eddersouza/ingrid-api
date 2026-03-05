namespace IP.Core.Api.Cities.UpdateCity;

internal sealed class UpdateCityCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateCityCommand, UpdateCityResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<UpdateCityResponse>> Handle(
        UpdateCityCommand command,
        CancellationToken cancellationToken)
    {
        CityId cityId = new(command.Id);

        City? city = await _cityRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == cityId,
            cancellationToken);

        if (city is null) return CityErrors.CityNotFound;

        city.Update(
             command.Request.Name,
             command.Request.IBGECode,
             StateId.Create(command.Request.StateId)
             );

        _cityRepository.Update(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateCityResponse(
            city.Id.Value!,
            city.Name.Value!,
            city.IBGECode,
            city.StateId.Value,
            $"Registro de Cidade [{city.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}
