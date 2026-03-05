namespace IP.AccIPInfo.Api.AccountSubscriptions.ActiveAccountSubscription;

internal sealed class ActiveAccountSubscriptionCommandHandler(IAccIPUoW _unitOfWork) :
    ICommandHandler<ActiveAccountSubscriptionCommand, ActiveAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<ActiveAccountSubscriptionResponse>> Handle(
        ActiveAccountSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        AccountSubscriptionId accountSubscriptionId = new(command.Id);

        AccountSubscription? accountSubscription = await _accountSubscriptionRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
            x => x.Id == accountSubscriptionId,
            cancellationToken);

        if (accountSubscription is null) return AccountSubscriptionErrors.AccountSubscriptionNotFound;

        bool isActived = command.Request.Active;
        string reason = command.Request.Reason;

        if (isActived && accountSubscription.ActivableInfo.Active)
            return AccountSubscriptionErrors.AlreadyActiveStatus(isActived);

        if (isActived) accountSubscription.ActivableInfo.SetAsActive();
        else accountSubscription.ActivableInfo.SetAsDeactive(reason);

        _accountSubscriptionRepository.Update(accountSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var actionText = isActived ? "ativado" : "desativado";

        var response = new ActiveAccountSubscriptionResponse(
            accountSubscription.Id.Value!,
            accountSubscription.Name.Value!,
            accountSubscription.ActivableInfo.Active,
            $"Registro de Plano {actionText} com sucesso!");

        return Result.Success(response);
    }
}