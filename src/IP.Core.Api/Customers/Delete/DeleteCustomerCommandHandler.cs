namespace IP.Core.Api.Customers.Delete;

internal sealed class DeleteCustomerCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<DeleteCustomerCommand, DeleteCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository =
        _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<DeleteCustomerResponse>> Handle(
        DeleteCustomerCommand command,
        CancellationToken cancellationToken)
    {
        CustomerId customerId = CustomerId.Create(command.Id);

        Customer? currentRecord = await _customerRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(Customer =>
                Customer.Id == customerId,
                cancellationToken);

        if (currentRecord is null) return CustomerErrors.NotFound;

        currentRecord.DeletableInfo.SetReason(command.Request.Reason);

        _customerRepository.Delete(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteCustomerResponse(
            $"Registro de Cliente [{currentRecord}] excluído com sucesso!");

        return Result.Success(response);
    }
}