namespace IP.Core.Api.Employees.Update;

internal class UpdateEmployeeEndpoint : ICoreEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/employees/{id:guid}", Call)
            .RequireAuthorization(EmployeeClaim.CanUpdate.Name)
            .MapEndpointProduces<UpdateEmployeeResponse>(true)
            .MapEndpointVersions(1)
            .MapEndpointDescription(
                "Alterar Colaborador",
                "Altera o registro de Colaborador.",
                "Colaboradores");
    }

    private static async Task<IResult> Call(HttpContext context,
        [FromRoute] Guid id,
        [FromBody] UpdateEmployeeRequest request,
        [FromServices] ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResponse> commandHandler,
        CancellationToken cancellationToken)
    {
        Result<UpdateEmployeeResponse> result =
            await commandHandler.Handle(new UpdateEmployeeCommand(id, request), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem, context);
    }
}