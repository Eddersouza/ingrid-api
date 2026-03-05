namespace IP.Core.Api.Addresses.GetAddress;

internal sealed class GetAddressEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/addresses/{id:guid}", Call)
            .RequireAuthorization(AddressClaim.CanRead.Name)
            .MapEndpointProduces<GetAddressResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Endereço",
                "Busca registro de Endereço",
                "Endereços");
    }
    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetAddressQuery, GetAddressResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetAddressResponse> result =
            await commandHandler.Handle(new GetAddressQuery(id), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
