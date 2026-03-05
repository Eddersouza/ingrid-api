namespace IP.AccCust.Api.AccountData.Integrate;

public class IntegrateAccountCommand :
    ICommand<IntegrateAccountResponse>;

public sealed class IntegrateAccountResponse(
    string message) :
    ResolvedData<object>(null, message);