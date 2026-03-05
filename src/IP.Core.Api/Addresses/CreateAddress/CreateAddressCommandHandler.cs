namespace IP.Core.Api.Addresses.CreateAddress;

internal class CreateAddressCommandHandler(ICoreUoW _unitOfWork) :
ICommandHandler<CreateAddressCommand, CreateAddressResponse>
{
    private readonly IAddressRepository _addressRepository =
        _unitOfWork.GetRepository<IAddressRepository>();

    private readonly ICityRepository _cityRepository =
        _unitOfWork.GetRepository<ICityRepository>();

    public async Task<Result<CreateAddressResponse>> Handle(
        CreateAddressCommand command,
        CancellationToken cancellationToken)
    {
        Address? address = await _addressRepository.Data()
            .AsNoTracking()
            .Include(s => s.City)
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name &&
                s.Neighborhood.Value == command.Request.Neighborhood &&
                s.CityId == CityId.Create(command.Request.CityId!.Value),
                cancellationToken);

        if (address != null) return AddressErrors.AddressAlreadyExists;
                
        var city = await _cityRepository.Entities
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == CityId.Create(command.Request.CityId!.Value), cancellationToken);

        if (city == null) return CityErrors.CityNotFound;

        address = Address.Create(
            command.Request.Name,
            command.Request.Neighborhood,
            command.Request.Code,
            CityId.Create(command.Request.CityId!.Value)
            );

        await _addressRepository.Create(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
                
        var response = new CreateAddressResponse(
            address.Id.Value,
            address.Name.Value,
            address.Code,
            address.CityId,
            $"Registro de Endereço [{address.Name.Value} - {address.Neighborhood.Value} - {city.Name.Value}] criado com sucesso!",
            address.ActivableInfo.Active);

        return await Task.FromResult(Result.Success(response));
    }
}
