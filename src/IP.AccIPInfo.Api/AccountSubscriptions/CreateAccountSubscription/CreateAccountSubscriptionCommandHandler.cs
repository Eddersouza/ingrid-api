namespace IP.AccIPInfo.Api.AccountSubscriptions.CreateAccountSubscription;

internal class CreateAccountSubscriptionCommandHandler(IAccIPUoW _unitOfWork) :
ICommandHandler<CreateAccountSubscriptionCommand, CreateAccountSubscriptionResponse>
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
        _unitOfWork.GetRepository<IAccountSubscriptionRepository>();

    public async Task<Result<CreateAccountSubscriptionResponse>> Handle(
        CreateAccountSubscriptionCommand command,
        CancellationToken cancellationToken)
    {
        AccountSubscription? accountSubscription = await _accountSubscriptionRepository.Data()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                s => s.Name.Value == command.Request.Name,
                cancellationToken);

        if (accountSubscription is not null) return AccountSubscriptionErrors.AccountSubscriptionAlreadyExists;

        accountSubscription = AccountSubscription.Create(
            command.Request.Name,
            command.Request.ExternalId
            );

        await _accountSubscriptionRepository.Create(accountSubscription);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CreateAccountSubscriptionResponse(
            accountSubscription.Id.Value,
            accountSubscription.Name.Value!,
            $"Registro de Plano [{accountSubscription.Name.Value}] criado com sucesso!",
            accountSubscription.ActivableInfo.Active,
            accountSubscription.ExternalId);

        return await Task.FromResult(Result.Success(response));
    }
}