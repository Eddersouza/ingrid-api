namespace IP.Core.Api.Customers.Update;

internal class UpdateCustomerEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/customers/{id:guid}", Call)
            .RequireAuthorization(CustomerClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateCustomerResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Cliente.",
                "Altera o registro de Cliente.",
                "Clientes");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateCustomerRequest request,
        [FromServices] ICommandHandler<UpdateCustomerCommand, UpdateCustomerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateCustomerResponse> result =
            await commandHandler.Handle(new UpdateCustomerCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}