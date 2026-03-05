namespace IP.Core.Api.Addresses.CreateAddress;

internal class CreateAddressEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/addresses", Call)
            .RequireAuthorization(AddressClaim.CanCreate.Name)
            .MapEndpointProduces<CreateAddressResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Endereço",
                "Cria novo registro de Endereço",
                "Endereços");
    }
    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateAddressRequest request,
        [FromServices] ICommandHandler<CreateAddressCommand, CreateAddressResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateAddressResponse> result =
            await commandHandler.Handle(new CreateAddressCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";
        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}
