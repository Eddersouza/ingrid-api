namespace IP.AccIPInfo.Api.AccountsIP.Update;

public sealed record UpdateAccountIPCommand(Guid Id, UpdateAccountIPRequest Request) :
    ICommand<UpdateAccountIPResponse>, ILoggableData;

public sealed class UpdateAccountIPRequest
{
    [Required]    
    public int Number { get; set; }

    [MinLength(AccountIPAlias.MAX_LENGTH)]
    public string? Alias { get; set; } = string.Empty;

    [Required]
    public AccountIPStatus? StatusCode { get; set; }

    [Required]
    public AccountIPType? TypeCode { get; set; }

    [Required]
    public RequestAccountIpChild Customer { get; set; } = default!;

    public RequestAccountIpChild? BusinessBranch { get; set; }
    public RequestAccountIpChild? BusinessSegment { get; set; }
    public RequestAccountIpChild? Integrator { get; set; }
    public bool OwnerIsIP { get; set; }
    public RequestAccountIpChild? Owner { get; set; }
    public RequestAccountIpChild? Retailer { get; set; }
    public RequestAccountIpChild? Subscription { get; set; }
}

public sealed class UpdateAccountIPResponse(
    AccountIP accountIP,
    string message) :
    ResolvedData<AccountIPResponseData>(new AccountIPResponseData(accountIP), message);