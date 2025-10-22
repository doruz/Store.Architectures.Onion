using Store.Infrastructure.Persistence.Cosmos;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class TestCosmosDatabase(
    CosmosDatabaseInitializer cosmosInitializer,
    CosmosDatabaseContainers cosmosContainers)
{
    public async Task EnsureIsCreated()
    {
        await cosmosInitializer.InitializeDatabase();
        await AddTestProducts();
    }

    public async Task EnsureIsDeleted()
    {
        if (cosmosContainers.Products.Database.Id.Contains("IntegrationTests"))
        {
            await cosmosContainers.Products.Database.DeleteAsync();
        }
    }

    public Task DeleteOrders(string accountId)
    {
        return cosmosContainers.Orders.DeleteAllItemsByPartitionKeyStreamAsync(accountId.ToPartitionKey());
    }

    private async Task AddTestProducts()
    {
        foreach (var product in TestProducts.All)
        {
            await cosmosContainers.Products.UpsertItemAsync(product);
        }
    }
}