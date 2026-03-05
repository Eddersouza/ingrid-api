namespace IP.IDI.Api.AppGuides.ToggleActive;

public sealed record ToggleActiveAppGuideCommand(Guid Id, ToggleActiveAppGuideRequest Request) :
    ICommand<ToggleActiveAppGuideResponse>, ILoggableData;

public sealed class ToggleActiveAppGuideRequest
{
    [Required]
    public bool Active { get; set; }
}

public sealed class ToggleActiveAppGuideResponse(ToggleActiveAppGuideResponseData data, string message) :
    ResolvedData<ToggleActiveAppGuideResponseData>(
        data, message);

public sealed record ToggleActiveAppGuideResponseData(
    Guid Id, string Name, string LinkId, int ViewCount, int TotalUsers, bool Active);