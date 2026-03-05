namespace IP.Core.Api.Addresses.UpdateAddress;

internal sealed class UpdateAddressCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateAddressCommand, UpdateAddressResponse>
{
    private readonly IAddressRepository _addressRepository =
        _unitOfWork.GetRepository<IAddressRepository>();

    public async Task<Result<UpdateAddressResponse>> Handle(
        UpdateAddressCommand command,
        CancellationToken cancellationToken)
    {
        AddressId addressId = new(command.Id);

        Address? address = await _addressRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == addressId,
            cancellationToken);

        if (address is null) return AddressErrors.AddressNotFound;

        address.Update(
             command.Request.Name,
             command.Request.Neighborhood,
             command.Request.Code,
             CityId.Create(command.Request.CityId)
             );

        Address? addressValidate = await _addressRepository.Data()
           .AsNoTracking()
           .Include(s => s.City)
           .FirstOrDefaultAsync(
               s => s.Name.Value == command.Request.Name &&
               s.Neighborhood.Value == command.Request.Neighborhood &&
               s.CityId == CityId.Create(command.Request.CityId),
               cancellationToken);

        if (addressValidate != null && 
            addressValidate!.Name == address.Name && 
            addressValidate.Neighborhood == address.Neighborhood &&
            addressValidate.CityId == address.CityId) return AddressErrors.AddressAlreadyExists;

        _addressRepository.Update(address);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateAddressResponse(
            address.Id.Value!,
            address.Name.Value!,
            address.Code,
            address.CityId.Value,
            $"Registro de Endereço [{address.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}

