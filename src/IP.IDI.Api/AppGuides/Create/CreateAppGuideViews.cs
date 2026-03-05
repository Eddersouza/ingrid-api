namespace IP.IDI.Api.AppGuides.Create;

public sealed record CreateAppGuideCommand(CreateAppGuideRequest Request) :
    ICommand<CreateAppGuideResponse>, ILoggableData;

public sealed class CreateAppGuideRequest
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

public sealed class CreateAppGuideResponse(CreateAppGuideResponseData data, string message) :
    ResolvedData<CreateAppGuideResponseData>(data, message);

public sealed record CreateAppGuideResponseData(
    Guid Id,
    string Name,
    string LinkId,
    int ViewCount,
    int TotalUsers,
    bool Active);