namespace IP.Core.Api.Cities.DeleteCity;

public sealed record DeleteCityCommand(Guid Id, DeleteCityRequest Request) :
    ICommand<DeleteCityResponse>, ILoggableData;

public sealed class DeleteCityRequest
{
    [Required]
    [MinLength(IEntityDeletable.REASON_MIN_LENGTH)]
    [MaxLength(IEntityDeletable.REASON_MAX_LENGTH)]
    public string Reason { get; set; } = null!;
}

public sealed class DeleteCityResponse(string message) :
    ResolvedData<object>(null, message);
