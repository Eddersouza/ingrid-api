namespace IP.Core.Api.Employees.ToggleActive;

internal sealed class ToggleActiveEmployeeCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<ToggleActiveEmployeeCommand, ToggleActiveEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<ToggleActiveEmployeeResponse>> Handle(
        ToggleActiveEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Id);

        Employee? currentRecord = await _employeeRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(employee =>
                employee.Id == employeeId,
                cancellationToken);

        if (currentRecord is null) return EmployeeErrors.NotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && currentRecord.ActivableInfo.Active)
            return EmployeeErrors.AlreadyActiveStatus(isActived);

        if (isActived) currentRecord.ActivableInfo.SetAsActive();
        else currentRecord.ActivableInfo.SetAsDeactive(reason);

        _employeeRepository.Update(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ToggleActiveEmployeeResponse(
            currentRecord,
            $"Registro de Colaborador {actionText} com sucesso!");

        return Result.Success(response);
    }
}