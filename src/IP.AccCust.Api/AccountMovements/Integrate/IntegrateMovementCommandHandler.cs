namespace IP.AccCust.Api.AccountMovements.Integrate;

internal class IntegrateMovementCommandHandler(
    IAccCustUoW accCustUoW,
    IAccIPExtUoW accIPExtUoW,
    IOnzVPNFunctions _onzVPN,    
    IAccountMovementSummaryService _movementSummaryService,
    ILogger<IntegrateMovementCommandHandler> _logger) :
    ICommandHandler<IntegrateMovementsCommand, IntegrateMovementsResponse>
{
    private const int HOURS_TO_INTERVAL_END_MOVEMENTS = 24;

    private readonly IAccountMovementViewRepository _movementViewRepository =
            accCustUoW.GetRepository<IAccountMovementViewRepository>();

    private readonly IAccountMovementSummaryRepository _summaryRepository =
            accIPExtUoW.GetRepository<IAccountMovementSummaryRepository>();

    public async Task<Result<IntegrateMovementsResponse>> Handle(
        IntegrateMovementsCommand command,
        CancellationToken cancellationToken)
    {
        DateTimeOffset today = DateTimeOffset.UtcNow.Date.ToUniversalTime();
        DateTimeOffset intervalStart = today.AddDays(-1).ToUniversalTime();
        DateTimeOffset lastDateToIntegrate = today.AddTicks(-1).ToUniversalTime();

        bool hasMovements = _summaryRepository.Entities.Any();
        await _onzVPN.OpenVPNAsync();

        // TODO: Remover apos teste inicial
        //if (!hasMovements)
        //    intervalStart = GetAdjustedIntervalStart();

        DateTimeOffset intervalEnd = GetAdjustedIntervalEnd(intervalStart);

        while (intervalEnd <= lastDateToIntegrate)
        {
            int skipItems = 0;
            int takeItems = 1000;

            _logger.LogInformation(
                "[{ServiceName}]:IntegrateMovements - Searching for movements to integrate between {IntervalStart} and {IntervalEnd} to integrate.[{StartItem}/{EndItem}]",
                nameof(IntegrateAccountCommandHandler),
                intervalStart,
                intervalEnd,
                skipItems,
                skipItems + takeItems);

            IEnumerable<AccountMovementSummary> movements =
                await _movementViewRepository.GetSummary(
                intervalStart,
                intervalEnd,
                takeItems,
                skipItems);

            _logger.LogInformation(
                "[{ServiceName}]:IntegrateMovements - Find {Count} movements to integrate between {IntervalStart} and {IntervalEnd} to integrate.[{StartItem}/{EndItem}]",
                nameof(IntegrateAccountCommandHandler),
                movements.Count(),
                intervalStart,
                intervalEnd,
                skipItems,
                skipItems + takeItems);

            while (movements.Any())
            {
                await _movementSummaryService.Integrate(movements);
                skipItems += takeItems;
                movements = await _movementViewRepository.GetSummary(
                    intervalStart,
                    intervalEnd,
                    takeItems,
                    skipItems);
            }

            intervalStart = GetAdjustedIntervalStart(intervalStart);
            intervalEnd = GetAdjustedIntervalEnd(intervalEnd);
        }

        await _onzVPN.CloseVPNAsync();

        return new IntegrateMovementsResponse("integração de movimentos realizado com sucesso!");
    }

    private static DateTimeOffset GetAdjustedIntervalEnd(DateTimeOffset interval) =>
        interval.AddHours(HOURS_TO_INTERVAL_END_MOVEMENTS).AddTicks(-1).ToUniversalTime();

    private static DateTimeOffset GetAdjustedIntervalStart(DateTimeOffset interval) =>
       interval.AddHours(HOURS_TO_INTERVAL_END_MOVEMENTS).ToUniversalTime();
}