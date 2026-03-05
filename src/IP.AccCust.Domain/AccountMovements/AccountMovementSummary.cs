using IP.AccCust.Domain.AccountsIP;

namespace IP.AccCust.Domain.AccountMovements;

public class AccountMovementSummary : EntityAuditable<Guid>
{
    public AccountMovementSummary()
    {
        Id = Guid.CreateVersion7();
    }

    public AccountMovementSummary(
        int accountNumber,
        decimal cancelAmount,
        int cancelQuantity,
        DateTime movementDateHour,
        decimal returnAmount,
        int returnAmountParcial,
        int returnQuantity,
        int returnQuantityParcial,
        decimal settledAmount,
        int settledQuantity) : this()
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

    public string AccountNumberTemp { 
        get { return AccountNumber.ToString(); } 
        set {
            _ = int.TryParse(value, out int parseResult);
            AccountNumber = parseResult; 
        } 
    }
    public int AccountNumber { get; private set; }

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

    public void AdjustHour() => MovementDateHour = MovementDateHour.AddHours(-3);

    public void SetAccountIP(AccountIPId accountIPId) => AccountIPId = accountIPId;

    public override string ToEntityName() => "Movimentos da Conta Totalizados";

    public override string ToString() => $"{AccountNumber} - {MovementDateHour}";
}