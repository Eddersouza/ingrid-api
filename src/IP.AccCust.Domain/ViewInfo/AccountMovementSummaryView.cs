namespace IP.AccCust.Domain.ViewInfo;

public class AccountMovementSummaryView
{
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Valor cancelado.
    /// </summary>
    public decimal CancelAmount { get; set; } = 0;

    /// <summary>
    /// Quantidade cancelado.
    /// </summary>
    public int CancelQuantity { get; set; } = 0;

    public DateTime MovementDateHour { get; set; } = DateTime.MinValue;    

    /// <summary>
    /// Valor devolvido.
    /// </summary>
    public decimal ReturnAmount { get; set; } = 0;

    /// <summary>
    /// Valor parcial devolvido.
    /// </summary>
    public int ReturnAmountParcial { get; set; } = 0;

    /// <summary>
    /// Quantidade devolvido.
    /// </summary>
    public int ReturnQuantity { get; set; } = 0;

    /// <summary>
    /// Quantidade parcial devolvido.
    /// </summary>
    public int ReturnQuantityParcial { get; set; } = 0;

    /// <summary>
    /// Valor liquidado.
    /// </summary>
    public decimal SettledAmount { get; set; } = 0;

    /// <summary>
    /// Quantidade liquidado.
    /// </summary>
    public int SettledQuantity { get; set; } = 0;
}