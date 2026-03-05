namespace IP.Core.Api.Addresses.GetAddresses;

internal class GetAddressesEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/addresses", Call)
            .RequireAuthorization(AddressClaim.CanList.Name)
            .MapEndpointProduces<GetAddressesResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Listar Endereços",
                "Lista registros de Endereço",
                "Endereços");
    }
    public static async Task<IResult> Call(HttpContext context,
       [AsParameters] GetAddressesQueryRequest request,
       [FromServices] IQueryHandler<GetAddressesQuery, GetAddressesResponse> commandHandler,
       CancellationToken cancellationToken)
    {
        Result<GetAddressesResponse> result =
            await commandHandler.Handle(new GetAddressesQuery(request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
