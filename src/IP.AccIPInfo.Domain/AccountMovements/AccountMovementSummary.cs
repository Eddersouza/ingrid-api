using IP.AccIPInfo.Domain.AccountsIP;

namespace IP.AccIPInfo.Domain.AccountMovements;

public class AccountMovementSummary : EntityAuditable<Guid>
{
    public AccountMovementSummary()
    {
        Id = Guid.CreateVersion7();
    }

    public AccountMovementSummary(
        int accountNumber,
        DateTime movementDateHour,
        int settledQuantity,
        decimal settledAmount,
        int cancelQuantity,
        decimal cancelAmount,
        int returnQuantity,
        decimal returnAmount,
        int returnQuantityParcial,
        decimal returnAmountParcial) : this()
    {
        AccountNumber = accountNumber;
        CancelAmount = cancelAmount;
        CancelQuantity = cancelQuantity;
        MovementDateHour = movementDateHour;
        ReturnAmount = returnAmount;
        ReturnAmountParcial = returnAmountParcial;
        ReturnQuantity = returnQuantity;
        ReturnQuantityParcial = returnQuantityParcial;
        SettledAmount = settledAmount;
        SettledQuantity = settledQuantity;
    }

    public virtual AccountIP AccountIP { get; private set; } = null!;

    public AccountIPId AccountIPId { get; private set; } = null!;

    public int AccountNumber { get; private set; } = 0;

    /// <summary>
    /// Valor cancelado.
    /// </summary>
    public decimal CancelAmount { get; private set; } = 0;

    /// <summary>
    /// Quantidade cancelado.
    /// </summary>
    public int CancelQuantity { get; private set; } = 0;

    public DateTime MovementDateHour { get; private set; } = DateTime.MinValue;

    /// <summary>
    /// Valor devolvido.
    /// </summary>
    public decimal ReturnAmount { get; private set; } = 0;

    /// <summary>
    /// Valor parcial devolvido.
    /// </summary>
    public decimal ReturnAmountParcial { get; private set; } = 0;

    /// <summary>
    /// Quantidade devolvido.
    /// </summary>
    public int ReturnQuantity { get; private set; } = 0;

    /// <summary>
    /// Quantidade parcial devolvido.
    /// </summary>
    public int ReturnQuantityParcial { get; private set; } = 0;

    /// <summary>
    /// Valor liquidado.
    /// </summary>
    public decimal SettledAmount { get; private set; } = 0;

    /// <summary>
    /// Quantidade liquidado.
    /// </summary>
    public int SettledQuantity { get; private set; } = 0;

    public void SetAccountIP(AccountIP accountIP) => AccountIP = accountIP;

    public override string ToEntityName() => "Movimentos da Conta Totalizados";

    public override string ToString() => $"{AccountNumber} - {MovementDateHour}";
}