namespace IP.Core.Api.Addresses.ActiveAddress;

internal class ActiveAddressEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/addresses/active/{id:guid}", Call)
            .RequireAuthorization(AddressClaim.CanActivateOrDeactivate.Name)
            .MapEndpointProduces<ActiveAddressResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Ativar/Desativar Endereço",
                "Ativa/Desativa registro de Endereço",
                "Endereços");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] ActiveAddressRequest request,
        [FromServices] ICommandHandler<ActiveAddressCommand, ActiveAddressResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<ActiveAddressResponse> result =
            await commandHandler.Handle(new ActiveAddressCommand(id, request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
