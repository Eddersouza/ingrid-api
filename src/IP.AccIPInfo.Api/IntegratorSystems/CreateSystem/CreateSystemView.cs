namespace IP.AccIPInfo.Api.IntegratorSystems.CreateSystem;

public sealed record CreateSystemCommand(CreateSystemRequest Request) :
    ICommand<CreateSystemResponse>, ILoggableData;

public sealed class CreateSystemRequest
{  
    [Required]
    [MinLength(IntegratorSystem.NAME_MIN_LENGTH)]
    [MaxLength(IntegratorSystem.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(IntegratorSystem.SITE_URL_MAX_LENGTH)]
    public string SiteUrl { get; set; } = string.Empty;

    [MaxLength(IntegratorSystem.DESCRIPTION_MAX_LENGTH)]
    public string Description { get; set; } = string.Empty;
}

public sealed class CreateSystemResponse(
    Guid id,
    string name,
    string siteUrl,
    string description,
    string message,
    bool active) :
    ResolvedData<CreateSystemResponseData>(
        new CreateSystemResponseData(id, name, siteUrl, description, active), message);

public sealed record CreateSystemResponseData(
    Guid Id,
    string Name,
    string SiteUrl,
    string Description,
    bool Active);