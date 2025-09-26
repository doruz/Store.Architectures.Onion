using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Microsoft.Azure.Cosmos.Linq;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosProductsRepository(CosmosDatabaseContainers containers) : IProductsRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return containers.Products
            .GetItemLinqQueryable<Product>(true)
            .AsEnumerable();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = id.ToPartitionKey(),
            MaxItemCount = 1
        };

        return await containers.Products
            .GetItemLinqQueryable<Product>(requestOptions: requestOptions)
            .Where(product => product.Id == id)
            .CountAsync() > 0;
    }

    public Task<Product?> FindAsync(string id)
        => containers.Products.FindAsync<Product>(id, id.ToPartitionKey());

    public async Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await containers.Products.CreateItemAsync(product, product.Id.ToPartitionKey());
    }


    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await containers.Products.ReplaceItemAsync(product, product.Id, product.Id.ToPartitionKey());
    }

    public async Task<bool> DeleteAsync(string id)
    {
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        return await containers.Products.DeleteAsync<Product>(id, id.ToPartitionKey());
    }
}