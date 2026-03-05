namespace IP.IDI.Api.AppGuides.Update;

public sealed record UpdateAppGuideCommand(Guid Id, UpdateAppGuideRequest Request) :
    ICommand<UpdateAppGuideResponse>, ILoggableData;

public sealed class UpdateAppGuideRequest
{
    [Required]
    [MinLength(AppGuide.NAME_MIN_LENGTH)]
    [MaxLength(AppGuide.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(AppGuide.LINK_ID_MIN_LENGTH)]
    [MaxLength(AppGuide.LINK_ID_MAX_LENGTH)]
    public string LinkId { get; set; } = string.Empty;
}

public sealed class UpdateAppGuideResponse(UpdateAppGuideResponseData data, string message) :
    ResolvedData<UpdateAppGuideResponseData>(data, message);

public sealed record UpdateAppGuideResponseData(
    Guid Id,
    string Name,
    string LinkId,
    int ViewCount,
    int TotalUsers,
    bool Active);