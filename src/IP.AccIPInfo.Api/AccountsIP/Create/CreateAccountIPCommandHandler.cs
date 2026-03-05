namespace IP.AccIPInfo.Api.AccountsIP.Create;

internal sealed class CreateAccountIPCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<CreateAccountIPCommand, CreateAccountIPResponse>
{
    private readonly IAccountIPRepository _AccountIPRepository =
        _unitOfWork.GetRepository<IAccountIPRepository>();

    public async Task<Result<CreateAccountIPResponse>> Handle(
        CreateAccountIPCommand command,
        CancellationToken cancellationToken)
    {
        CreateAccountIPRequest request = command.Request;

        AccountIPCustomer customer =
            new(request.Customer.Id, request.Customer.Name);

        AccountIPAlias? alias =
            request.Alias.IsNotNullOrWhiteSpace() ?
            new(request.Alias!) : null;

        AccountIP accountIP = AccountIP.Create(
            request.Number,
            alias,
            customer,
            request.StatusCode,
            request.TypeCode);

        RequestAccountIpChild bussinesBranch = command.Request.BusinessBranch!;
        RequestAccountIpChild businessSegment = command.Request.BusinessSegment!;
        if (bussinesBranch != null)
            accountIP.AddBusinessBranch(
                new AccountIPBusinessBranchSegment(
                    bussinesBranch.Id,
                    bussinesBranch.Name,
                    businessSegment.Id,
                    businessSegment.Name));

        RequestAccountIpChild integrator = command.Request.Integrator!;
        if (integrator != null) accountIP.AddIntegrator(
            new AccountIPIntegratorSistem(integrator.Id, integrator.Name));

        RequestAccountIpChild owner = command.Request.Owner!;
        if (request.OwnerIsIP)
            accountIP.AddOwner(new AccountIPOwner(request.OwnerIsIP));

        if (owner != null)
            accountIP.AddOwner(new AccountIPOwner(owner.Id, owner.Name));

        RequestAccountIpChild retailer = command.Request.Retailer!;
        if (retailer != null) accountIP.AddRetailer(
            new AccountIPRetailer(retailer.Id, retailer.Name));

        RequestAccountIpChild subsctiption = command.Request.Subscription!;
        if (subsctiption != null) accountIP.AddSubscription(
            new AccountIPAccountSubscription(subsctiption.Id, subsctiption.Name));

        await _AccountIPRepository.Create(accountIP);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        CreateAccountIPResponse response = new(
            accountIP,  
            $"Registro de Conta [{accountIP}] criado com sucesso!");

        return Result.Success(response);
    }
}