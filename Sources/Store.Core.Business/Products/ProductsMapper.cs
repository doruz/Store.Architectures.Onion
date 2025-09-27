using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

internal static class ProductsMapper
{
    public static ProductModel ToProductModel(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };

    public static Product ToProduct(this ProductEditModel model) => new()
    {
        Name = model.Name,
        Price = model.Price
    };
}