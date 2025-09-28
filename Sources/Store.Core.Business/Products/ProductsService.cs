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

    public Task<ProductModel?> FindProductAsync(string id) =>
        repositories.Products
            .FindAsync(id)
            .MapAsync(ProductsMapper.ToProductModel);

    public async Task<ProductModel> CreateAsync(NewProductModel productModel)
    {
        var newProduct = productModel.ToProduct();

        await repositories.Products.AddAsync(newProduct);

        return newProduct.ToProductModel();
    }

    public async Task<bool> UpdateAsync(string id, EditProductModel productModel)
    {
        var existingProduct = await repositories.Products.FindAsync(id);
        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Update(productModel.Name, productModel.Price, productModel.Stock);

        await repositories.Products.UpdateAsync(existingProduct);

        return true;
    }

    // TODO: to mark product as deleted
    public Task<bool> DeleteAsync(string id) => repositories.Products.DeleteAsync(id);
}