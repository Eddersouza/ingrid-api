namespace IP.Core.Api.Addresses.ActiveAddress;

public sealed record ActiveAddressCommand(Guid Id, ActiveAddressRequest Request) :
    ICommand<ActiveAddressResponse>, ILoggableData;

public sealed class ActiveAddressRequest
{
    [Required]
    public bool Active { get; set; }

    [MinLength(IEntityActivable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityActivable.REASON_MAX_LENGTH)]
    [Description("Campo obrigatório se Active=false, não informar se Active=true")]
    public string Reason { get; set; } = string.Empty;
}

public sealed class ActiveAddressResponse(Guid id, string name, bool active, string message) :
    ResolvedData<ActiveAddressResponseData>(
        new ActiveAddressResponseData(id, name, active), message);

public sealed record ActiveAddressResponseData(Guid Id, string Name, bool Active);
