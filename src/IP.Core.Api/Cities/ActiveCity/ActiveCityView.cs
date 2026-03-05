namespace IP.Core.Api.Cities.ActiveCity;

public sealed record ActiveCityCommand(Guid Id, ActiveCityRequest Request) :
    ICommand<ActiveCityResponse>, ILoggableData;

public sealed class ActiveCityRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ActiveCityResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveCityResponseData>(
        new ActiveCityResponseData(id, name, active), message);

public sealed record ActiveCityResponseData(Guid Id, string Name, bool Active);
