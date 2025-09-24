using EnsureThat;
using Store.Core.Domain;
using Store.Core.Repositories;

namespace Store.Infrastructure.Repositories;

internal sealed class ProductsInMemoryRepository : IProductsRepository
{
    private readonly List<Product> products = 
    [
        new Product
        {
            Id = "9952a5da-9945-11f0-9ae5-2f38b5112d0d",
            Name = "Apple",
            Price = Price.Euro(2.5)
        },
        new Product
        {
            Id = "9952a5da-9945-11f0-9ae5-2f38b5112d0d",
            Name = "Orange",
            Price = Price.Euro(1.99)
        }
    ];

    public async Task<IEnumerable<Product>> GetAllAsync() 
        => products;

    public async Task<Product?> FindAsync(string id)
        => products.FirstOrDefault(p => p.Id == id);

    public async Task AddAsync(Product product) 
        => products.Add(EnsureArg.IsNotNull(product, nameof(product)));

    public async Task UpdateAsync(Product product)
    {
        await DeleteAsync(product.Id);
        await AddAsync(product);
    }

    public async Task DeleteAsync(string id)
        => products.RemoveAll(p => p.Id == id);
}