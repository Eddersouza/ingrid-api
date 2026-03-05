namespace IP.Core.Api.Employees.Delete;

internal sealed class DeleteEmployeeCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository =
        _unitOfWork.GetRepository<IEmployeeRepository>();

    public async Task<Result<DeleteEmployeeResponse>> Handle(
        DeleteEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        EmployeeId employeeId = EmployeeId.Create(command.Id);

        Employee? currentRecord = await _employeeRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(employee =>
                employee.Id == employeeId,
                cancellationToken);

        if (currentRecord is null) return EmployeeErrors.NotFound;

        currentRecord.DeletableInfo.SetReason(command.Request.Reason);

        _employeeRepository.Delete(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteEmployeeResponse(
            $"Registro de Colaborador [{currentRecord}] excluído com sucesso!");

        return Result.Success(response);
    }
}