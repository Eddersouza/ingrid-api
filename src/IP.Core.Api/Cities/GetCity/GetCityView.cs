namespace IP.Core.Api.Cities.GetCity;

public sealed record GetCityQuery(Guid Id) :
    IQuery<GetCityResponse>;

public sealed class GetCityResponse(
        Guid id, string name, string ibgeCode, Guid stateId, string stateName, bool active) :
    ResolvedData<GetCityResponseData>(
        new GetCityResponseData(id, name, ibgeCode, stateId, stateName, active), string.Empty);

public sealed record GetCityResponseData(
    Guid Id, string Name, string IBGECode, Guid StateId, string StateName, bool Active);

