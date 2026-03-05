namespace IP.AccIPInfo.Api.IntegratorSystems.UpdateSystem;

internal sealed record UpdateSystemCommand(Guid Id, UpdateSystemRequest Request) :
    ICommand<UpdateSystemResponse>, ILoggableData;

public class UpdateSystemRequest
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

public sealed class UpdateSystemResponse(
    Guid id,
    string name,
    string siteUrl,
    string description,
    string message) :
    ResolvedData<UpdateSystemResponseData>(
        new UpdateSystemResponseData(id, name, siteUrl, description), message)
{ };

public sealed record UpdateSystemResponseData(
    Guid Id, string Name, string SiteUrl, string Description);