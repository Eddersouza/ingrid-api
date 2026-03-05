namespace IP.AccCust.Api.AccountData.Integrate;

public interface IAccountSubscriptionService
{
    Task<bool> HasPlans(CancellationToken cancellationToken);

    Task Integrate(List<PlanView> planViews, CancellationToken cancellationToken);
}

internal sealed class AccountSubscriptionService(
    IAccIPExtUoW accIPUoW,
    ILogger<AccountSubscriptionService> _logger) :
    IAccountSubscriptionService
{
    private readonly IAccountSubscriptionRepository _accountSubscriptionRepository =
      accIPUoW.GetRepository<IAccountSubscriptionRepository>();

    public async Task<bool> HasPlans(CancellationToken cancellationToken) =>
            await _accountSubscriptionRepository.Entities.AnyAsync(cancellationToken);

    public async Task Integrate(List<PlanView> planViews, CancellationToken cancellationToken)
    {
        {
            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - Started to integrate {Count} plans",
                   nameof(AccountSubscriptionService),
                   planViews.Count);

            int count = 0;
            int total = planViews.Count;
            foreach (PlanView planView in planViews)
            {
                string planId = planView.PlanId.ToString();
                string planName = planView.PlanName;
                string planStatus = planView.PlanStatus;
                string planUpdateDate = planView.UpdateDate.ToString();

                try
                {

                    AccountSubscription? accountSubscription = await _accountSubscriptionRepository.Entities
                        .FirstOrDefaultAsync(x => x.ExternalId == planId, cancellationToken);

                    if (accountSubscription == null)
                        count = await PlanAddToContext(count, total, planId, planName, planStatus);
                    else
                        count = PlanUpdateInContext(count, total, planName, planStatus, accountSubscription);
                    await accIPUoW.SaveChangesAsync(cancellationToken);
                }
                catch (Exception exception)
                {
                    _logger.LogError(
                        "[{ServiceName}]:IntegrateAccount - integrate plan {planId} for date {planDate} failed: {StackTrace}",
                       nameof(AccountSubscriptionService),
                       planId,
                       planUpdateDate,
                       exception.GetInnerExceptions());
                }
            }

            _logger.LogInformation(
                "[{ServiceName}]:IntegrateAccount - End to integrate plans",
                   nameof(AccountSubscriptionService));
        }
    }

    private async Task<int> PlanAddToContext(
        int count,
        int total,
        string planId,
        string planName,
        string planStatus)
    {
        AccountSubscription accountSubscription;
        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - create {Count}/{Total} plan",
            nameof(AccountSubscriptionService),
            ++count,
            total);

        accountSubscription =
            AccountSubscription.Create(planName, planId);

        if (planStatus != "ACTIVE") accountSubscription.ActivableInfo.SetAsDeactive();

        await _accountSubscriptionRepository.Create(accountSubscription);
        return count;
    }

    private int PlanUpdateInContext(
            int count,
        int total,
        string planName,
        string planStatus,
        AccountSubscription accountSubscription)
    {
        _logger.LogInformation(
            "[{ServiceName}]:IntegrateAccount - update {Count}/{Total} plan",
            nameof(AccountSubscriptionService),
            ++count,
            total);

        accountSubscription.SetName(planName);
        if (planStatus != "ACTIVE") accountSubscription.ActivableInfo.SetAsDeactive();
        if (planStatus == "ACTIVE") accountSubscription.ActivableInfo.SetAsActive();

        _accountSubscriptionRepository.Update(accountSubscription);

        return count;
    }
}