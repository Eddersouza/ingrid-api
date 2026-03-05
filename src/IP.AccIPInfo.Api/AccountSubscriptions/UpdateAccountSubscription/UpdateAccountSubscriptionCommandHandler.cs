namespace IP.AccIPInfo.Api.AccountSubscriptions.UpdateAccountSubscription;

internal sealed class UpdateAccountSubscriptionCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<UpdateAccountSubscriptionCommand, UpdateAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<UpdateAccountSubscriptionResponse>> Handle(
        UpdateAccountSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        AccountSubscriptionId accountSubscriptionId = new(command.Id);

        AccountSubscription? accountSubscription = await _accountSubscriptionRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == accountSubscriptionId,
            cancellationToken);

        if (accountSubscription is null) return AccountSubscriptionErrors.AccountSubscriptionNotFound;

        accountSubscription.Update(
             command.Request.Name
             );

        _accountSubscriptionRepository.Update(accountSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateAccountSubscriptionResponse(
            accountSubscription.Id.Value!,
            accountSubscription.Name.Value!,
            $"Registro de Plano [{accountSubscription.Name.Value}] alterado com sucesso!");

        return Result.Success(response);
    }
}