namespace IP.Core.Api.Customers.Create;

internal sealed class CreateCustomerCommandHandler(ICoreUoW _unitOfWork) :
    ICommandHandler<CreateCustomerCommand, CreateCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository =
        _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<CreateCustomerResponse>> Handle(
        CreateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.Request;

        Customer customer = Customer.Create(
            request.PersonTypeCode!.Value,
            new CustomerName(request.Name),
            new CustomerTradingName(request.TradingName),
            new CPFOrCNPJ(request.DocumentNumber),
            request.StatusCode!.Value);

        if (!customer.DocumentNumber.IsValid()) return CPFOrCNPJ.Invalid;

        await _customerRepository.Create(customer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateCustomerResponse(
            customer,
            $"Registro de Cliente [{customer.Name.Value}] criado com sucesso!");

        return Result.Success(response);
    }
}