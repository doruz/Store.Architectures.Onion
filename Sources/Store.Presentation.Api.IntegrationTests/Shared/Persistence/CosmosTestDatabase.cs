using Store.Core.Shared;
using Store.Infrastructure.Persistence.Cosmos;

namespace Store.Presentation.Api.IntegrationTests;

internal sealed class CosmosTestDatabase(
    CosmosDatabaseInitializer cosmosInitializer,
    CosmosDatabaseContainers cosmosContainers)
{
    public async Task EnsureIsCreated()
    {
        await cosmosInitializer.Execute();
        await ProductsTestData.All.ForEachAsync(p => cosmosContainers.Products.UpsertItemAsync(p));
    }

    public async Task EnsureIsDeleted()
    {
        if (cosmosContainers.Products.Database.Id.Contains("IntegrationTests"))
        {
            await cosmosContainers.Products.Database.DeleteAsync();
        }
    }
}