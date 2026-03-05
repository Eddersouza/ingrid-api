namespace IP.AccCust.Api.AccountData.Integrate;

internal sealed class IntegrateAccountCommandHandler(
    IAccCustUoW uow,
    IOnzVPNFunctions _onzVPN,
    IAccountAndCustomerService _customerService,
    IAccountSubscriptionService _accountSubscriptionService,
    ILogger<IntegrateAccountCommandHandler> _logger) :
    ICommandHandler<IntegrateAccountCommand, IntegrateAccountResponse>
{
    private readonly IAccountViewRepository _accountViewRepository =
           uow.GetRepository<IAccountViewRepository>();

    private readonly IPlanViewRepository _planViewRepository =
            uow.GetRepository<IPlanViewRepository>();

    public async Task<Result<IntegrateAccountResponse>> Handle(
        IntegrateAccountCommand command,
        CancellationToken cancellationToken)
    {
        bool hasPlans = await _accountSubscriptionService.HasPlans(cancellationToken);
        bool hasCustomers = await _customerService.HasCustomers(cancellationToken);
        await _onzVPN.OpenVPNAsync();

        await IntegratePlans(hasPlans, cancellationToken);
        await IntegrateCustomerAndAccounts(hasCustomers, cancellationToken);

        await _onzVPN.CloseVPNAsync();

        return new IntegrateAccountResponse("Integração de Contas e Planos realizado com sucesso!");
    }

    private async Task<List<AccountView>> GetRangeAccounts(
        int skipItems,
        int takeItems,
        DateTimeOffset minimalDateToGetItems,
        CancellationToken cancellationToken) =>
        await _accountViewRepository.Entities
            .Where(x => x.AccountUpdatedAt > minimalDateToGetItems ||
                x.PersonUpdatedAt > minimalDateToGetItems)
            .OrderBy(x => x.AccountUpdatedAt)
            .Skip(skipItems)
            .Take(takeItems)
            .ToListAsync(cancellationToken);

    private async Task<List<PlanView>> GetRangePlans(
       int skipItems,
       int takeItems,
       DateTimeOffset minimalDateToGetItems,
       CancellationToken cancellationToken) =>
       await _planViewRepository.Entities
           .Where(x => x.UpdateDate > minimalDateToGetItems)
           .OrderBy(x => x.UpdateDate)
           .Skip(skipItems)
           .Take(takeItems)
           .ToListAsync(cancellationToken);

    private async Task IntegrateCustomerAndAccounts(bool hasCustomers, CancellationToken cancellationToken)
    {
        int skipItems = 0;
        int takeItems = 1000;

        DateTimeOffset minimalDateToGetItems = hasCustomers ?
            DateTimeOffset.UtcNow.Date.AddDays(-1).ToUniversalTime() :
            DateTimeOffset.MinValue.ToUniversalTime();

        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - Searching for accounts after date {Date} to integrate",
            nameof(IntegrateAccountCommandHandler),
            minimalDateToGetItems);

        List<AccountView> accountViews =
             await GetRangeAccounts(skipItems, takeItems, minimalDateToGetItems, cancellationToken);

        int round = 0;

        while (accountViews.Count > 0)
        {
            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - Started to integrate customer/account round: {Round}",
                nameof(IntegrateAccountCommandHandler),
                ++round);
            await _customerService.Integrate(accountViews, cancellationToken);

            skipItems += takeItems;
            accountViews = await GetRangeAccounts(skipItems, takeItems, minimalDateToGetItems, cancellationToken);
        }
    }

    private async Task IntegratePlans(bool hasPlans, CancellationToken cancellationToken)
    {
        int skipItems = 0;
        int takeItems = 1000;

        DateTimeOffset minimalDateToGetItems = hasPlans ?
            DateTimeOffset.UtcNow.Date.AddDays(-1).ToUniversalTime() :
            DateTimeOffset.MinValue.ToUniversalTime();

        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - Searching for plans after date {Date} to integrate",
            nameof(IntegrateAccountCommandHandler),
            minimalDateToGetItems);


        List<PlanView> planViews =
            await GetRangePlans(skipItems, takeItems, minimalDateToGetItems, cancellationToken);

        int round = 0;

        while (planViews.Count > 0)
        {
            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - Started to integrate plans round: {Round}",
                nameof(IntegrateAccountCommandHandler),
                ++round);
            await _accountSubscriptionService.Integrate(planViews, cancellationToken);

            skipItems += takeItems;
            planViews = await GetRangePlans(skipItems, takeItems, minimalDateToGetItems, cancellationToken);
        }
    }
}