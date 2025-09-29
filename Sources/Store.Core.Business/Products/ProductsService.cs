using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public sealed class ProductsService(RepositoriesContext repositories)
{
    public Task<IEnumerable<ProductModel>> GetAllAvailable() =>
        repositories.Products
            .FilterAsync(product => product.Stock > 0)
            .SelectAsync(ProductsMapper.ToProductModel);

    public Task<IEnumerable<ProductModel>> GetAll() =>
        repositories.Products
            .GetAllAsync()
            .SelectAsync(ProductsMapper.ToProductModel);

    public Task<ProductModel> FindProductAsync(string id) =>
        repositories.Products
            .FindAsync(id)
            .EnsureIsNotNull(id)
            .MapAsync(ProductsMapper.ToProductModel);

    public async Task<ProductModel> CreateAsync(NewProductModel productModel)
    {
        var newProduct = productModel.ToProduct();

        await repositories.Products.AddAsync(newProduct);

        return newProduct.ToProductModel();
    }

    public async Task UpdateAsync(string id, EditProductModel productModel)
    {
        var existingProduct = await repositories.Products
            .FindAsync(id)
            .EnsureIsNotNull(id);
      
        existingProduct.Update(productModel.Name, productModel.Price, productModel.Stock);

        await repositories.Products.UpdateAsync(existingProduct);
    }

    public async Task DeleteAsync(string id)
    {
        if (await repositories.Products.ExistsAsync(id) is false)
        {
            throw ProductErrors.NotFound(id);
        }

        await repositories.Products.DeleteAsync(id);
    }
}