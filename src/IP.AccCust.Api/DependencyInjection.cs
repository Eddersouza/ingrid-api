namespace IP.AccCust.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddAccCustServices(
        this IServiceCollection services)
    {
        services.AddScoped<IAccountSubscriptionService, AccountSubscriptionService>();
        services.AddScoped<IAccountAndCustomerService, AccountAndCustomerService>();
        services.AddScoped<IAccountMovementSummaryService, AccountMovementSummaryService>();

        return services;
    }
}