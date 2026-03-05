namespace IP.Core.Api.Addresses.UpdateAddress;

internal class UpdateAddressEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/addresses/{id:guid}", Call)
            .RequireAuthorization(AddressClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateAddressResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Endereço",
                "Altera registro de Endereço",
                "Endereços");
    }
    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateAddressRequest request,
        [FromServices] ICommandHandler<UpdateAddressCommand, UpdateAddressResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateAddressResponse> result =
            await commandHandler.Handle(new UpdateAddressCommand(id, request), cancellationToken);
        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}
