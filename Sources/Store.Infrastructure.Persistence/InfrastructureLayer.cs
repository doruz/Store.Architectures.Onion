global using Store.Core.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;
using Store.Infrastructure.Persistence.Cosmos;
using Store.Infrastructure.Persistence.InMemory;

namespace Store.Infrastructure.Persistence;

public static class InfrastructureLayer
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.UseCosmos())
        {
            services.AddCosmosPersistence(configuration);
        }
        else
        {
            services.AddInMemoryPersistence();
        }

        return services.AddSingleton<RepositoriesContext>();
    }

    private static bool UseCosmos(this IConfiguration configuration)
        => configuration["AllowedPersistence"].IsEqualTo("cosmos");

    private static IServiceCollection AddCosmosPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CosmosOptions>(configuration.GetRequiredSection(nameof(CosmosOptions)).Bind)

            .AddSingleton(CosmosClientFactory.Create(configuration))
            .AddSingleton<CosmosDatabaseContainers>()
            .AddTransient<IAppInitializer, CosmosDatabaseInitializer>()

            .AddSingleton<IProductsRepository, CosmosProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>();

    private static IServiceCollection AddInMemoryPersistence(this IServiceCollection services) =>
        services
            .AddSingleton<InMemoryDatabase>()
            .AddSingleton<IAppInitializer, InMemoryCollectionsInitializer>()

            .AddSingleton<IProductsRepository, InMemoryProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, InMemoryShoppingCartsRepository>();
}