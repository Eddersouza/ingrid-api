namespace IP.Core.Api.States.CreateState;

public sealed record CreateStateCommand(CreateStateRequest Request) :
    ICommand<CreateStateResponse>, ILoggableData;

public sealed class CreateStateRequest
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

public sealed class CreateStateResponse(
    Guid id, 
    string ibgeCode,
    string code,
    string name, 
    string message) :
    ResolvedData<CreateStateResponseData>(
        new CreateStateResponseData(id, ibgeCode, code, name), message);

public sealed record CreateStateResponseData(
    Guid Id,
    string IBGECode, 
    string Code, 
    string Name);