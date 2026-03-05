namespace IP.Core.Api.Cities.CreateCity;

public sealed record CreateCityCommand(CreateCityRequest Request) :
    ICommand<CreateCityResponse>, ILoggableData;

public sealed class CreateCityRequest
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
    public Guid StateId { get; set; } = default!;
}

public sealed class CreateCityResponse(
    Guid id,
    string ibgeCode,
    string name,
    StateId stateId,
    string message,
    bool active) :
    ResolvedData<CreateCityResponseData>(
        new CreateCityResponseData(id, ibgeCode, name, stateId.Value, active), message);

public sealed record CreateCityResponseData(
    Guid Id,
    string IBGECode,
    string Name,
    Guid StateId,
    bool Active);
