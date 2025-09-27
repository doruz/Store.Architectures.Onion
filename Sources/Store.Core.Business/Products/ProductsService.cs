using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public sealed class ProductsService(RepositoriesContext repositories)
{
    public Task<IEnumerable<ProductModel>> GetAll() =>
        repositories.Products
            .GetAllAsync()
            .SelectAsync(ProductsMapper.ToProductModel);

    public Task<ProductModel?> FindProductAsync(string id) =>
        repositories.Products
            .FindAsync(id)
            .MapAsync(ProductsMapper.ToProductModel);


    public async Task<ProductModel> Create(ProductEditModel productModel)
    {
        var newProduct = productModel.ToProduct();

        await repositories.Products.AddAsync(newProduct);

        return newProduct.ToProductModel();
    }

    public async Task<bool> Update(string id, ProductEditModel productModel)
    {
        var existingProduct = await repositories.Products.FindAsync(id);
        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = productModel.Name;
        existingProduct.Price = productModel.Price;

        await repositories.Products.UpdateAsync(existingProduct);

        return true;
    }

    public Task<bool> Delete(string id) 
        => repositories.Products.DeleteAsync(id);
}