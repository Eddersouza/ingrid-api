using Dapper;

namespace IP.AccCust.Persistence.AccountMovementsView;

public interface IAccountMovementViewRepository :
    IQueryableRepository<AccountMovementView>
{
    Task<IEnumerable<AccountMovementSummary>> GetSummary(
        DateTimeOffset intervalStart,
        DateTimeOffset intervalEnd,
        int take,
        int skip);
}

internal class AccountMovementRepositoryView(AccCustDbContext appContext) :
    RepositoryBase<AccountMovementView>(appContext), IAccountMovementViewRepository
{
    public async Task<IEnumerable<AccountMovementSummary>> GetSummary(
        DateTimeOffset intervalStart,
        DateTimeOffset intervalEnd,
        int take,
        int skip)
    {
        var connection = appContext.Database.GetDbConnection();

        return await connection.QueryAsync<AccountMovementSummary>(
            QueryToSummary,
            new
            {
                intervalStart,
                intervalEnd,
                take,
                skip
            });
    }

    private const string QueryToSummary = @"WITH base AS (
  SELECT
    COALESCE(NULLIF(LTRIM(movement_payee_account_number, '0'), ''), '0') AS account_number_norm,
    date_trunc('hour', movement_created_at) AS data_formatada,
    movement_payee_ispb,
    transaction_status,
    movement_type,
    movement_amount,
    movement_msg_end_to_end_id  -- útil se quiser auditar por TXID
  FROM bancodigital_bi.vw_sub_movement
  WHERE
   movement_created_at >= @intervalStart
  AND movement_created_at <  @intervalEnd),
agg AS (
  SELECT
    account_number_norm                     AS account_number,
    data_formatada,
    /* LIQUIDADOS: contam e somam apenas créditos efetivos */
    COUNT(*) FILTER (
      WHERE transaction_status = '2 - LIQUIDATED' AND movement_type = 'CREDIT'
    )                                       AS total_qtd_liquidado,
    SUM(CASE
          WHEN transaction_status = '2 - LIQUIDATED' AND movement_type = 'CREDIT'
            THEN movement_amount
          ELSE 0
        END)                                AS total_valor_liquidado,
    /* DEVOLVIDOS integrais: linhas de refund vêm como DEBIT com valor negativo */
    COUNT(*) FILTER (
      WHERE transaction_status = '5 - REFUNDED' AND movement_type = 'DEBIT'
    )                                       AS total_qtd_devolvido,
    SUM(CASE
          WHEN transaction_status = '5 - REFUNDED' AND movement_type = 'DEBIT'
            THEN ABS(movement_amount)   -- reporta valor devolvido como positivo
          ELSE 0
        END)                                AS total_valor_devolvido,
    /* DEVOLUÇÕES PARCIAIS */
    COUNT(*) FILTER (
      WHERE transaction_status = '6 - PARTIALLY_REFUNDED' AND movement_type = 'DEBIT'
    )                                       AS total_qtd_parcial_devolvido,
    SUM(CASE
          WHEN transaction_status = '6 - PARTIALLY_REFUNDED' AND movement_type = 'DEBIT'
            THEN ABS(movement_amount)
          ELSE 0
        END)                                AS total_valor_parcial_devolvido,
    /* CANCELADOS: considerar apenas o DEBIT (o estorno em CREDIT não soma no cancelado) */
    COUNT(*) FILTER (
      WHERE transaction_status = '0 - CANCELED' AND movement_type = 'DEBIT'
    )                                       AS total_qtd_cancelado,
    SUM(CASE
          WHEN transaction_status = '0 - CANCELED' AND movement_type = 'DEBIT'
            THEN ABS(movement_amount)
          ELSE 0
        END)                                AS total_valor_cancelado
  FROM base
  GROUP BY
  account_number_norm,
  data_formatada
)
select
  account_number as AccountNumberTemp,
  data_formatada as MovementDateHour,
  total_qtd_liquidado as SettledQuantity,
  total_valor_liquidado as SettledAmount,
  total_valor_devolvido as ReturnAmount,
  total_qtd_devolvido as ReturnQuantity,
  total_qtd_parcial_devolvido as ReturnQuantityParcial,
  total_valor_parcial_devolvido as ReturnAmountParcial,
  total_qtd_cancelado as CancelQuantity,
  total_valor_cancelado as CancelAmount
FROM agg
ORDER by account_number, data_formatada
LIMIT @take
OFFSET @skip;";
}