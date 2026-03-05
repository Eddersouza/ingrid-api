namespace IP.Core.Api.Employees.GetTeamById;

internal class GetEmployeeTeamEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees/{id:guid}/team/select", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetEmployeeTeamResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Equipe do Colaborador",
                "Busca Equipe do Colaborador",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [AsParameters] GetEmployeeTeamQueryRequest request,
        [FromServices] IQueryHandler<GetEmployeeTeamQuery, GetEmployeeTeamResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetEmployeeTeamResponse> result =
            await commandHandler.Handle(new GetEmployeeTeamQuery(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}