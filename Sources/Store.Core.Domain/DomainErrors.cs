namespace Store.Core.Domain;

internal static class DomainErrors
{
    internal static class Products
    {
        public static AppError StockNotAvailable(string id)
            => AppError.Conflict("product_stock_not_available", id);
    }
}