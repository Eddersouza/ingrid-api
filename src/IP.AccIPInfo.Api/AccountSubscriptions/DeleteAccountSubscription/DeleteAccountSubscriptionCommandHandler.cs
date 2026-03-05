namespace IP.AccIPInfo.Api.AccountSubscriptions.DeleteAccountSubscription;

internal sealed class DeleteAccountSubscriptionCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<DeleteAccountSubscriptionCommand, DeleteAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<DeleteAccountSubscriptionResponse>> Handle(
       DeleteAccountSubscriptionCommand command,
       CancellationToken cancellationToken)
    {
        AccountSubscriptionId accountSubscriptionId = new(command.Id);

        AccountSubscription? accountSubscription = await _accountSubscriptionRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == accountSubscriptionId,
            cancellationToken);

        if (accountSubscription is null) return AccountSubscriptionErrors.AccountSubscriptionNotFound;

        accountSubscription.DeletableInfo.SetReason(command.Request.Reason);

        _accountSubscriptionRepository.Delete(accountSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new DeleteAccountSubscriptionResponse(
            $"Registro de Plano [{accountSubscription.Name.Value}] excluído com sucesso!");

        return Result.Success(response);
    }
}