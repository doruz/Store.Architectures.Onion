using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Products;

internal static class ProductErrors
{
    public static Product EnsureIsNotNull(this Product? product, string productId) 
        => product ?? throw AppError.NotFound("product_not_found", productId);

    public static Product EnsureStockIsAvailable(this Product product, int quantity) =>
        product.StockIsNotAvailable(quantity)
            ? throw AppError.Conflict("product_stock_not_available", product.Id)
            : product;
}