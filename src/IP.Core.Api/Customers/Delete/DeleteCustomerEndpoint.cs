namespace IP.Core.Api.Customers.Delete;

internal class DeleteCustomerEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/customers/{id:guid}", Call)
            .RequireAuthorization(CustomerClaim.CanDelete.Name)
            .MapEndpointProduces<DeleteCustomerResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Cliente",
                "Remover registro de Cliente",
                "Clientes");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteCustomerRequest request,
        [FromServices] ICommandHandler<DeleteCustomerCommand, DeleteCustomerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteCustomerResponse> result =
            await commandHandler.Handle(new DeleteCustomerCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}