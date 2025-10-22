using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Infrastructure.Persistence.Cosmos;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class TestCosmosDatabase(CosmosDatabaseContainers cosmosContainers)
{
    public async Task EnsureIsInitialized()
    {
        await DeleteTestProducts();
        await AddTestProducts();
    }

    public Task DeleteCustomerOrders(string customerId)
    {
        EnsureIsTestDatabase();

        return cosmosContainers.Orders.DeleteAllItemsByPartitionKeyStreamAsync(customerId.ToPartitionKey());
    }

    private async Task AddTestProducts()
    {
        EnsureIsTestDatabase();

        foreach (var product in TestProducts.All)
        {
            await cosmosContainers.Products.UpsertItemAsync(product);
        }
    }

    private async Task DeleteTestProducts()
    {
        EnsureIsTestDatabase();

        var products = cosmosContainers.Products
            .GetItemLinqQueryable<Product>(true)
            .AsEnumerable();


        foreach (var product in products)
        {
            await cosmosContainers.Products.DeleteAsync<Product>(product.Id, product.Id.ToPartitionKey());
        }
    }

    private void EnsureIsTestDatabase()
        => EnsureArg.IsTrue(cosmosContainers.Products.Database.Id.Contains("IntegrationTests"));
}