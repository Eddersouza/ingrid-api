namespace IP.Core.Api.Cities.UpdateCity;

internal sealed record UpdateCityCommand(Guid Id, UpdateCityRequest Request) :
    ICommand<UpdateCityResponse>, ILoggableData;

public class UpdateCityRequest
{
    [Required]
    [MinLength(City.NAME_MIN_LENGTH)]
    [MaxLength(City.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(City.IBGE_CODE_MIN_LENGTH)]
    [MaxLength(City.IBGE_CODE_MAX_LENGTH)]
    public string IBGECode { get; set; } = string.Empty;

    [Required]
    public Guid StateId { get; set; } = Guid.Empty;
}
public sealed class UpdateCityResponse(
    Guid id,
    string name,
    string ibgeCode,
    Guid stateId,
    string message) :
    ResolvedData<UpdateCityResponseData>(
        new UpdateCityResponseData(id, name, ibgeCode, stateId), message) { };

public sealed record UpdateCityResponseData(
    Guid Id, string Name, string IBGECode, Guid StateId);
