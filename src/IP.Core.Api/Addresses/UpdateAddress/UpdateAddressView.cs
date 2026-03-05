namespace IP.Core.Api.Addresses.UpdateAddress;

internal sealed record UpdateAddressCommand(Guid Id, UpdateAddressRequest Request) :
    ICommand<UpdateAddressResponse>, ILoggableData;

public class UpdateAddressRequest
{
    [Required]
    [MinLength(Address.NAME_MIN_LENGTH)]
    [MaxLength(Address.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(Address.CODE_MIN_LENGTH)]
    [MaxLength(Address.CODE_MAX_LENGTH)]
    public string Code { get; set; } = string.Empty;

    [Required]
    [MinLength(Address.NEIGHBORHOOD_MIN_LENGTH)]
    [MaxLength(Address.NEIGHBORHOOD_MAX_LENGTH)]
    public string Neighborhood { get; set; } = string.Empty;

    [Required]
    public Guid CityId { get; set; } = Guid.Empty;
}
public sealed class UpdateAddressResponse(
    Guid id,
    string name,
    string code,
    Guid cityId,
    string message) :
    ResolvedData<UpdateAddressResponseData>(
        new UpdateAddressResponseData(id, name, code, cityId), message)
{ };

public sealed record UpdateAddressResponseData(
    Guid Id, string Name, string Code, Guid CityId);

