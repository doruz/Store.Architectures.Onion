global using Store.Core.Shared;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Domain.Repositories;
using Store.Infrastructure.Persistence.Cosmos;

namespace Store.Infrastructure.Persistence;

public static class InfrastructureLayer
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddCosmosPersistence(configuration)
            .AddSingleton<RepositoriesContext>();

    public static IServiceCollection AddCosmosPersistence(this IServiceCollection services, IConfiguration configuration) =>
        services
            .Configure<CosmosOptions>(configuration.GetRequiredSection(nameof(CosmosOptions)).Bind)

            .AddSingleton(CosmosClientFactory.Create(configuration))
            .AddSingleton<CosmosDatabaseContainers>()
            .AddTransient<IAppInitializer, CosmosDatabaseInitializer>()

            .AddSingleton<IProductsRepository, CosmosProductsRepository>()
            .AddSingleton<IShoppingCartsRepository, CosmosShoppingCartsRepository>();
}