using IP.AccIPInfo.Api.Transactions;
using Microsoft.Extensions.DependencyInjection;


namespace IP.AccIPInfo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountInfoServices(
        this IServiceCollection services)
    {
        services.AddScoped<ITransactionSummaryService, TransactionSummaryService>();
        return services;
    }
}