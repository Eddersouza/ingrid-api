namespace IP.Core.Api.Customers.Create;

internal class CreateCustomerEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/customers", Call)
            .RequireAuthorization(CustomerClaim.CanCreate.Name)
            .MapEndpointProduces<CreateCustomerResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Criar Cliente",
                "Cria novo registro de Cliente.",
                "Clientes");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromBody] CreateCustomerRequest request,
        [FromServices] ICommandHandler<CreateCustomerCommand, CreateCustomerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<CreateCustomerResponse> result =
            await commandHandler.Handle(new CreateCustomerCommand(request), cancellationToken);

        string location = string.Empty;
        if (result.IsSuccess) location = $"{context.Request.Path}/{result.Value?.Data?.Id}";

        return result.Match(Results.Created, CustomResults.Problem, context, location);
    }
}