namespace IP.Core.Api.Cities.DeleteCity;

internal sealed class DeleteCityCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteCityCommand, DeleteCityResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();
    public async Task<Result<DeleteCityResponse>> Handle(
       DeleteCityCommand command,
       CancellationToken cancellationToken)
    {
        CityId cityId = new(command.Id);

        City? city = await _cityRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == cityId,
            cancellationToken);

        if (city is null) return CityErrors.CityNotFound;

        city.DeletableInfo.SetReason(command.Request.Reason);

        _cityRepository.Delete(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteCityResponse(
            $"Registro de Cidade [{city.Name.Value}] excluído com sucesso!");

        return Result.Success(response);
    }

}
