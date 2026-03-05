namespace IP.Core.Api.Customers.GetById;

internal class GetCustomerEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/{id:guid}", Call)
            .RequireAuthorization(CustomerClaim.CanRead.Name)
            .MapEndpointProduces<GetCustomerResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Cliente",
                "Busca registro de Cliente",
                "Clientes");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetCustomerQuery, GetCustomerResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetCustomerResponse> result =
            await commandHandler.Handle(new GetCustomerQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}