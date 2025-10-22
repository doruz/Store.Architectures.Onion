using Store.Core.Business;
using Store.Core.Shared;
using Store.Infrastructure.Persistence;

public static class ApiLayer
{
    public static IServiceCollection AddCurrentSolution(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddBusiness()
            .AddPersistence(configuration)

            .AddTransient<ICurrentCustomer, CurrentCustomer>()
            .AddHostedService<AppInitializationService>();
    }
}