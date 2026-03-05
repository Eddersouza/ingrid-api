namespace IP.Core.Api.Customers.GetById;

internal sealed class GetCustomerQueryHandler(
    ICoreUoW _unitOfWork) :
    IQueryHandler<GetCustomerQuery, GetCustomerResponse>
{
    private readonly ICustomerRepository _customerRepository =
        _unitOfWork.GetRepository<ICustomerRepository>();

    public async Task<Result<GetCustomerResponse>> Handle(
        GetCustomerQuery query,
        CancellationToken cancellationToken)
    {
        CustomerId CustomerId = CustomerId.Create(query.Id);

        Customer? currentRecord = await _customerRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(Customer =>
                Customer.Id == CustomerId,
                cancellationToken);

        if (currentRecord is null) return CustomerErrors.NotFound;

        return Result.Success(new GetCustomerResponse(currentRecord!));
    }
}