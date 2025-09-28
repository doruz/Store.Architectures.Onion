using EnsureThat;

namespace Store.Core.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }

    public Price Price { get; set; } = 0;

    public int Stock { get; set; }

    public void Update(string? name, decimal? price, int? stock)
    {
        Name = name ?? Name;
        Price = price ?? Price;
        Stock = stock ?? Stock;
    }

    public void DecreaseStock(int quantity)
    {
        EnsureStockIsAvailable(quantity);
        Stock -= quantity;
    }

    internal void EnsureStockIsAvailable(int quantity)
    {
        if (quantity.IsNotInRange(0, Stock))
        {
            throw AppError.Conflict("product_stock_not_available", Id);
        }
    }
}