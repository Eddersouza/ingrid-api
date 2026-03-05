namespace IP.Core.Api.Customers.GetById;

public sealed record GetCustomerQuery(Guid Id) :
    IQuery<GetCustomerResponse>;

public sealed class GetCustomerResponse(
    Customer customer) :
    ResolvedData<CustomerResponseData>(
        new CustomerResponseData(customer), string.Empty);