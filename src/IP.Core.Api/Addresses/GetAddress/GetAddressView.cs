namespace IP.Core.Api.Addresses.GetAddress;

public sealed record GetAddressQuery(Guid Id) :
    IQuery<GetAddressResponse>;

public sealed class GetAddressResponse(
        Guid id, string name, string neighborhood, string code, string cityId, string cityName, string stateCode, bool active) :
    ResolvedData<GetAddressResponseData>(
        new GetAddressResponseData(id, name, neighborhood, code, new GetAddressCityData(cityId, cityName), stateCode, active), string.Empty);

public sealed record GetAddressResponseData(
    Guid Id, string Name, string Neighborhood, string Code, GetAddressCityData City, string StateCode, bool Active);

public sealed record GetAddressCityData(string Id, string Name);