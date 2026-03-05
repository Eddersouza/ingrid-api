namespace IP.AccIPInfo.Api.AccountsIP.Update;

internal sealed class UpdateAccountIPCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<UpdateAccountIPCommand, UpdateAccountIPResponse>
{
    private readonly IAccountIPRepository _accountIPRepository =
        _unitOfWork.GetRepository<IAccountIPRepository>();

    public async Task<Result<UpdateAccountIPResponse>> Handle(
        UpdateAccountIPCommand command,
        CancellationToken cancellationToken)
    {
        AccountIPId accountIPId = AccountIPId.Create(command.Id);

        AccountIP? currentRecord = await _accountIPRepository
            .Entities.AsNoTracking()
            .FirstOrDefaultAsync(AccountIP =>
                AccountIP.Id == accountIPId,
                cancellationToken);

        if (currentRecord is null) return AccountIPErrors.NotFound;

        UpdateAccountIPRequest request = command.Request;

        currentRecord.SetNumber(request.Number);
        currentRecord.SetStatusCode(request.StatusCode!);
        currentRecord.SetTypeCode(request.TypeCode!);

        AccountIPCustomer customer =
           new(request.Customer.Id, request.Customer.Name);
        currentRecord.SetCustomer(customer);

        AccountIPAlias? alias = new(request.Alias!);
        currentRecord.SetAlias(alias);

        RequestAccountIpChild bussinesBranch = command.Request.BusinessBranch!;
        RequestAccountIpChild businessSegment = command.Request.BusinessSegment!;

        currentRecord.AddBusinessBranch(
            new AccountIPBusinessBranchSegment(
                bussinesBranch?.Id,
                bussinesBranch?.Name!,
                businessSegment?.Id,
                businessSegment?.Name!));

        RequestAccountIpChild? integrator = command.Request.Integrator;
        currentRecord.AddIntegrator(
            new AccountIPIntegratorSistem(integrator?.Id, integrator?.Name!));

        RequestAccountIpChild? owner = command.Request.Owner;

        if (request.OwnerIsIP)
            currentRecord.AddOwner(new AccountIPOwner(request.OwnerIsIP));
        else
            currentRecord.AddOwner(new AccountIPOwner(owner?.Id, owner?.Name));

        RequestAccountIpChild? retailer = command.Request.Retailer;
        currentRecord.AddRetailer(
            new AccountIPRetailer(retailer?.Id, retailer?.Name!));

        RequestAccountIpChild? subsctiption = command.Request.Subscription;
        currentRecord.AddSubscription(
            new AccountIPAccountSubscription(subsctiption?.Id, subsctiption?.Name!));

        _accountIPRepository.Update(currentRecord);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateAccountIPResponse(
            currentRecord,
            $"Registro de Conta [{currentRecord}] alterado com sucesso!");

        return await Task.FromResult(Result.Success(response));
    }
}