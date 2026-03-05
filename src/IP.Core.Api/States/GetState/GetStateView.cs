namespace IP.Core.Api.States.GetState;

public sealed record GetStateQuery(Guid Id) :
    IQuery<GetStateResponse>;

public sealed class GetStateResponse(
    Guid id, string name, string ibgeCode, string code) :
    ResolvedData<GetStateResponseData>(
        new GetStateResponseData(id, name, ibgeCode, code), string.Empty);

public sealed record GetStateResponseData(
    Guid Id, string IBGECode, string Code, string Name);