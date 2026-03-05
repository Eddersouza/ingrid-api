namespace IP.AccIPInfo.Api.AccountsIP.Update;

internal class UpdateAccountIPEndpoint : IAccIPEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/accounts/{id:guid}", Call)
            .RequireAuthorization(AccountIPClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateAccountIPResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Conta",
                "Altera o registro de Conta.",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateAccountIPRequest request,
        [FromServices] ICommandHandler<UpdateAccountIPCommand, UpdateAccountIPResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateAccountIPResponse> result =
            await commandHandler.Handle(new UpdateAccountIPCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}