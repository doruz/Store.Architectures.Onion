using System.Net;
using EnsureThat;
using Microsoft.Azure.Cosmos;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosProductsRepository(CosmosDatabaseContainers containers) : IProductsRepository
{
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> FindAsync(string id)
    {
        EnsureArg.IsNotNullOrEmpty(id, nameof(id));

        try
        {
            var entity = await containers.Products.ReadItemAsync<Product>(id, new PartitionKey(id));
            return entity;
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return default;
        }
    }

    public async Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await containers.Products.CreateItemAsync(product, new (product.Id));
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}