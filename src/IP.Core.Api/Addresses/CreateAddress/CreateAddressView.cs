namespace IP.Core.Api.Addresses.CreateAddress;

public sealed record CreateAddressCommand(CreateAddressRequest Request) :
    ICommand<CreateAddressResponse>, ILoggableData;

public sealed class CreateAddressRequest
{
    [Required]
    [MinLength(Address.CODE_MIN_LENGTH)]
    [MaxLength(Address.CODE_MAX_LENGTH)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MinLength(Address.NAME_MIN_LENGTH)]
    [MaxLength(Address.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(Address.NEIGHBORHOOD_MAX_LENGTH)]
    [MaxLength(Address.NEIGHBORHOOD_MIN_LENGTH)]
    public string Neighborhood { get; set; } = string.Empty;

    [Required]
    public Guid? CityId { get; set; } = default!;
}

public sealed class CreateAddressResponse(
    Guid id,
    string name,
    string code,
    CityId cityId,
    string message,
    bool active) :
    ResolvedData<CreateAddressResponseData>(
        new CreateAddressResponseData(id, name, code, cityId.Value, active), message);

public sealed record CreateAddressResponseData(
    Guid Id,
    string Name,
    string Code,
    Guid CityId,
    bool Active);