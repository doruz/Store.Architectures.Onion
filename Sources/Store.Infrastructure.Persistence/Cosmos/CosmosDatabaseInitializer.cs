using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosDatabaseInitializer(CosmosClient cosmosClient, IOptions<CosmosOptions> options)
    : IAppInitializer
{
    private static readonly List<ContainerProperties> Containers =
    [
        new ContainerProperties(CosmosDatabaseContainers.ProductsName, "/id"),
        new ContainerProperties(CosmosDatabaseContainers.ShoppingCartsName, "/id"),
        new ContainerProperties(CosmosDatabaseContainers.OrdersName, "/accountId")
    ];

    async Task IAppInitializer.Execute()
        => await InitializeContainers(await InitializeDatabase());

    public async Task<Database> InitializeDatabase()
    {
        var throughput = ThroughputProperties.CreateAutoscaleThroughput(options.Value.MaxThroughput);

        return await cosmosClient.CreateDatabaseIfNotExistsAsync(options.Value.DatabaseName, throughput);
    }

    private async Task InitializeContainers(Database database)
    {
        foreach (var container in Containers)
        {
            await database.CreateContainerIfNotExistsAsync(container);
        }
    }
}