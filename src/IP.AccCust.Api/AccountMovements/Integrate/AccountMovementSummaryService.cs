namespace IP.AccCust.Api.AccountMovements.Integrate;

public interface IAccountMovementSummaryService
{
    Task Integrate(IEnumerable<AccountMovementSummary> accountMovements);
}

internal sealed class AccountMovementSummaryService(
    IAccIPExtUoW accIPExtUoW,
    ILogger<AccountMovementSummaryService> _logger) :
    IAccountMovementSummaryService
{
    private readonly IAccountIPRepository _accountIPrepository =
        accIPExtUoW.GetRepository<IAccountIPRepository>();

    private readonly IAccountMovementSummaryRepository _summaryRepository =
           accIPExtUoW.GetRepository<IAccountMovementSummaryRepository>();

    public async Task Integrate(IEnumerable<AccountMovementSummary> accountMovements)
    {
        int count = 1;
        int total = accountMovements.Count();

        IEnumerable<int> accountsList = accountMovements.Select(x => x.AccountNumber).Distinct();

        Dictionary<int, AccountIPId> accountsNumberInMovements =
            await _accountIPrepository.Entities
            .Where(account => accountsList.Contains(account.Number)).ToDictionaryAsync(account => account.Number, account => account.Id);

        foreach (AccountMovementSummary accountMovement in accountMovements)
        {
            bool hasAccount = accountsNumberInMovements.TryGetValue(accountMovement.AccountNumber, out AccountIPId? accountIPId);

            if (!hasAccount)
            {
                _logger.LogWarning(
                    "[{ServiceName}]:IntegrateMovements - [{ItemNumber}/{TotalItems}] Dont find account {AccountNumber} to integrate movements to {MovementDateHour}",
                    nameof(IntegrateAccountCommandHandler),
                    count,
                    total,
                    accountMovement.AccountNumber,
                    accountMovement.MovementDateHour);

                count++;

                continue;
            }

            accountMovement.SetAccountIP(accountsNumberInMovements[accountMovement.AccountNumber]);
            accountMovement.AdjustHour();

            try
            {
                _logger.LogInformation(
                    "[{ServiceName}]:IntegrateMovements - [{ItemNumber}/{TotalItems}] Integrating account {AccountNumber} to {MovementDateHour}",
                    nameof(IntegrateAccountCommandHandler),
                    count,
                    total,
                    accountMovement.AccountNumber,
                    accountMovement.MovementDateHour);

                await _summaryRepository.Create(accountMovement);

                accIPExtUoW.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    "[{ServiceName}]:IntegrateMovements - [{ItemNumber}/{TotalItems}] Error Integrating account {AccountNumber} to {MovementDateHour} : {StackTrace}",
                    nameof(IntegrateAccountCommandHandler),
                    count,
                    total,
                    accountMovement.AccountNumber,
                    accountMovement.MovementDateHour,
                    exception.GetInnerExceptions());
            }
            finally
            {
                count++;
            }
        }
    }
}