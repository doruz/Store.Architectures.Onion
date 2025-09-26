using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

public sealed class ProductsService(IProductsRepository productsRepository)
{
    public Task<IEnumerable<ProductReadModel>> GetAllAsync() =>
        productsRepository
            .GetAllAsync()
            .SelectAsync(ProductsMapper.ToReadModel);

    public Task<ProductReadModel?> FindProductAsync(string id) =>
        productsRepository
            .FindAsync(id)
            .MapAsync(ProductsMapper.ToReadModel);


    public async Task<ProductReadModel> CreateAsync(ProductWriteModel productModel)
    {
        var newProduct = productModel.ToProduct();

        await productsRepository.AddAsync(newProduct);

        return newProduct.ToReadModel();
    }

    public async Task<bool> UpdateAsync(string id, ProductWriteModel productModel)
    {
        var existingProduct = await productsRepository.FindAsync(id);
        if (existingProduct is null)
        {
            return false;
        }

        existingProduct.Name = productModel.Name;
        existingProduct.Price = productModel.Price;

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
}