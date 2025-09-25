using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public sealed class ProductsService(IProductsRepository productsRepository)
{
    public Task<IEnumerable<ProductReadModel>> GetAllAsync() =>
        productsRepository
            .GetAllAsync()
            .SelectAsync(ToReadModel);

    public Task<ProductReadModel?> FindProductAsync(string id) =>
        productsRepository
            .FindAsync(id)
            .MapAsync(ToReadModel);


    public async Task<ProductReadModel> CreateAsync(ProductWriteModel product)
    {
        var newProduct = new Product
        {
            Name = product.Name,
            Price = product.Price
        };

        await productsRepository.AddAsync(newProduct);

        return ToReadModel(newProduct);
    }

    public async Task<bool> UpdateAsync(string id, ProductWriteModel product)
    {
        var existingProduct = await productsRepository.FindAsync(id);
        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var existingProduct = await productsRepository.FindAsync(id);
        if (existingProduct is null)
        {
            return false;
        }

        await productsRepository.DeleteAsync(id);

        return true;
    }

    private static ProductReadModel ToReadModel(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };
}