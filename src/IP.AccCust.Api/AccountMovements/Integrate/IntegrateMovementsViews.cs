namespace IP.AccCust.Api.AccountMovements.Integrate;

public class IntegrateMovementsCommand :
    ICommand<IntegrateMovementsResponse>;

public sealed class IntegrateMovementsResponse(
    string message) :
    ResolvedData<object>(null, message);