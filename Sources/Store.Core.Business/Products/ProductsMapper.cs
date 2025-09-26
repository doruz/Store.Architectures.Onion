using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

internal static class ProductsMapper
{
    public static ProductReadModel ToReadModel(this Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price
    };

    public static Product ToProduct(this ProductWriteModel model) => new()
    {
        Name = model.Name,
        Price = model.Price
    };
}