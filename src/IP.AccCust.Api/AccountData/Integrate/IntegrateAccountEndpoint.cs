namespace IP.AccCust.Api.AccountData.Integrate;

internal sealed class IntegrateAccountEndpoint : IAccCustEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/accounts", Call)
            //.RequireAuthorization(AccountIPClaim.CanCreate.Name)
            .MapEndpointProduces<object>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Integrar Contas",
                "Incializa o processo de integração de contas.",
                "Contas");
    }

    private static async Task<IResult> Call(HttpContext context,
        CancellationToken cancellationToken)
    {
        return Results.Ok();
    }
}