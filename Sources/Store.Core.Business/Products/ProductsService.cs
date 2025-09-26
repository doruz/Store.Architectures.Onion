using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public sealed class ProductsService(RepositoriesContext repositories)
{
    public Task<IEnumerable<ProductReadModel>> GetAllAsync() =>
        repositories.Products
            .GetAllAsync()
            .SelectAsync(ProductsMapper.ToReadModel);

    public Task<ProductReadModel?> FindProductAsync(string id) =>
        repositories.Products
            .FindAsync(id)
            .MapAsync(ProductsMapper.ToReadModel);


    public async Task<ProductReadModel> CreateAsync(ProductWriteModel productModel)
    {
        var newProduct = productModel.ToProduct();

        await repositories.Products.AddAsync(newProduct);

        return newProduct.ToReadModel();
    }

    public async Task<bool> UpdateAsync(string id, ProductWriteModel productModel)
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

    public Task<bool> DeleteAsync(string id) 
        => repositories.Products.DeleteAsync(id);
}