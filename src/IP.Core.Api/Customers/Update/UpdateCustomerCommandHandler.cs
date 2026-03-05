namespace IP.Core.Api.Customers.Update;

internal sealed class UpdateCustomerCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<UpdateCustomerCommand, UpdateCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository =
        _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<UpdateCustomerResponse>> Handle(
        UpdateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        CustomerId CustomerId = CustomerId.Create(command.Id);
        UpdateCustomerRequest request = command.Request;

        Customer? currentRecord = await _customerRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(Customer =>
                Customer.Id == CustomerId,
                cancellationToken);

        if (currentRecord is null) return CustomerErrors.NotFound;

        currentRecord.SetPersonType(request.PersonTypeCode!.Value);
        currentRecord.SetName(new CustomerName(request.Name));
        currentRecord.SetTradingName(new CustomerTradingName(request.TradingName));
        currentRecord.SetDocumentNumber(new CPFOrCNPJ(request.DocumentNumber));
        currentRecord.SetStatus(request.StatusCode!.Value);

        _customerRepository.Update(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateCustomerResponse(
            currentRecord,
            $"Registro de Cliente [{currentRecord.Name.Value}] alterado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}