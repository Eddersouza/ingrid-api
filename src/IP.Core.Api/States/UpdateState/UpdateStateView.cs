namespace IP.Core.Api.States.UpdateState;

internal sealed record UpdateStateCommand(Guid Id, UpdateStateRequest Request) :
    ICommand<UpdateStateResponse>, ILoggableData;

public sealed class UpdateStateRequest
{
    [Required]
    [MinLength(State.NAME_MIN_LENGTH)]
    [MaxLength(State.NAME_MAX_LENGTH)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MinLength(State.IBGE_CODE_MIN_LENGTH)]
    [MaxLength(State.IBGE_CODE_MAX_LENGTH)]
    public string IBGECode { get; set; } = string.Empty;

    [Required]
    [MinLength(State.CODE_MIN_LENGTH)]
    [MaxLength(State.CODE_MAX_LENGTH)]
    public string Code { get; set; } = string.Empty;

}

public sealed class UpdateStateResponse(
    Guid id,
    string IBGECode,
    string Code,
    string name,
    string message) :
    ResolvedData<UpdateStateResponseData>(
        new UpdateStateResponseData(id, IBGECode, Code, name), message);

public sealed record UpdateStateResponseData(
    Guid Id, string IBGECode, string Code, string Name);

