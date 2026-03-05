namespace IP.Core.Api.Cities.ActiveCity;

internal sealed class ActiveCityCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<ActiveCityCommand, ActiveCityResponse>
{
    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<ActiveCityResponse>> Handle(
        ActiveCityCommand command,
        CancellationToken cancellationToken)
    {
        CityId cityId = new(command.Id);

        City? city = await _cityRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == cityId,
            cancellationToken);

        if (city is null) return CityErrors.CityNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && city.ActivableInfo.Active)
            return CityErrors.AlreadyActiveStatus(isActived);

        if (isActived) city.ActivableInfo.SetAsActive();
        else city.ActivableInfo.SetAsDeactive(reason);

        _cityRepository.Update(city);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveCityResponse(
            city.Id.Value!,
            city.Name.Value!,
            city.ActivableInfo.Active,
            $"Registro de Cidade {actionText} com sucesso!");

        return Result.Success(response);
    }
}
