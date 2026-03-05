namespace IP.Core.Api.Employees.GetByUserId;

internal class GetEmployeeByUserEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/employees/user/{id:guid}", Call)
            .RequireAuthorization()
            .MapEndpointProduces<GetEmployeeByUserResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Busca Colaborador por Id de Usuário",
                "Busca registro de Colaborador por Id de Usuário",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromServices] IQueryHandler<GetEmployeeByUserQuery, GetEmployeeByUserResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<GetEmployeeByUserResponse> result =
            await commandHandler.Handle(new GetEmployeeByUserQuery(id), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}