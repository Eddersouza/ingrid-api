namespace IP.Core.Api.Addresses.GetAddress;

internal sealed class GetAddressQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetAddressQuery, GetAddressResponse>
{
    private readonly IAddressRepository _addressRepository =
        _unitOfWork.GetRepository<IAddressRepository>();

    public async Task<Result<GetAddressResponse>> Handle(
        GetAddressQuery query,
        CancellationToken cancellationToken)
    {
        AddressId id = new(query.Id);
        Address? currentAddress =
            await _addressRepository.Data()
            .Include(s => s.City)
            .ThenInclude(s => s.State)
            .AsNoTracking()
            .SingleAsync(s => s.Id == id, cancellationToken);

        if (currentAddress is null) return AddressErrors.AddressNotFound;

        GetAddressResponse response = new(
            currentAddress.Id.Value!,
            currentAddress.Name.Value!,
            currentAddress.Neighborhood.Value!,
            currentAddress.Code!,
            currentAddress.City.Id!.Value!.ToString(),
            currentAddress.City.Name.Value!,
            currentAddress.City.State.Code!,
            currentAddress.ActivableInfo.Active);

        return Result.Success(response);
    }
}
