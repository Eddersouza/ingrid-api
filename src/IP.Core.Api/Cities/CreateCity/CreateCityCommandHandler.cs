namespace IP.Core.Api.Cities.CreateCity;

internal class CreateCityCommandHandler(ICoreUoW _unitOfWork) :
ICommandHandler<CreateCityCommand, CreateCityResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<CreateCityResponse>> Handle(
        CreateCityCommand command,
        CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name,
                cancellationToken);

        if (city is not null) return CityErrors.CityAlreadyExists;            

        if (command.Request.StateId == Guid.Empty) return StateErrors.StateNotFound;

        city = City.Create(
            command.Request.IBGECode,
            command.Request.Name,
            StateId.Create(command.Request.StateId)            
            );

        await _cityRepository.Create(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateCityResponse(
            city.Id.Value,
            city.IBGECode,
            city.Name.Value!,
            city.StateId,
            $"Registro de Cidade [{city.Name.Value}] criado com sucesso!",
            city.ActivableInfo.Active);

        return await Task.FromResult(Result.Success(response));
    }
}
