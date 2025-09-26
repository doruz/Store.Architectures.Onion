global using Store.Core.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;
using Store.Infrastructure.Persistence.Cosmos;
using Store.Infrastructure.Persistence.InMemory;

namespace Store.Infrastructure.Persistence;

public static class InfrastructureLayer
{
    // TODO: to add a configuration in app settings
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            //.AddInMemoryPersistence()
            .AddCosmosPersistence(configuration)
            .AddSingleton<RepositoriesContext>();


    private static IServiceCollection AddCosmosPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CosmosOptions>(configuration.GetRequiredSection(nameof(CosmosOptions)).Bind)

            .AddSingleton(CosmosClientFactory.Create(configuration))
            .AddSingleton<CosmosDatabaseContainers>()
            .AddTransient<IAppInitializer, CosmosDatabaseInitializer>()

            .AddSingleton<IProductsRepository, CosmosProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>();

    public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services) =>
        services
            .AddSingleton<InMemoryCollections>()
            .AddSingleton<IAppInitializer, InMemoryCollectionsInitializer>()
            .AddSingleton<IProductsRepository, InMemoryProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, InMemoryShoppingCartsRepository>();
}