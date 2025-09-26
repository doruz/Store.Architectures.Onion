using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryProductsRepository(InMemoryCollections collections) : IProductsRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync() 
        => collections.Products;

    public async Task<bool> ExistsAsync(string id)
        => collections.Products.Any(p => p.Id.IsEqualTo(id));

    public async Task<Product?> FindAsync(string id)
        => collections.Products.Find(p => p.Id.IsEqualTo(id));

    public async Task AddAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        collections.Products.Add(product);
    }

    public async Task UpdateAsync(Product product)
    {
        EnsureArg.IsNotNull(product, nameof(product));

        await DeleteAsync(product.Id);
        await AddAsync(product);
    }

    public async Task<bool> DeleteAsync(string id)
        => collections.Products.RemoveAll(p => p.Id.IsEqualTo(id)) > 0;
}