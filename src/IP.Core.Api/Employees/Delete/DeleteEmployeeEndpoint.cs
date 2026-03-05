namespace IP.Core.Api.Employees.Delete;

internal class DeleteEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("/employees/{id:guid}", Call)
            .RequireAuthorization(EmployeeClaim.CanDelete.Name)
            .MapEndpointProduces<DeleteEmployeeResponse>()
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Excluir Colaborador",
                "Remover registro de Colaborador",
                "Colaboradores");
    }

    private static async Task<IResult> Call(
        HttpContext context,
        [FromRoute] Guid id,
        [FromBody] DeleteEmployeeRequest request,
        [FromServices] ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<DeleteEmployeeResponse> result =
            await commandHandler.Handle(new DeleteEmployeeCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}