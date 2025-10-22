namespace Store.Core.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }

    public required Price Price { get; set; }

    public required int Stock { get; set; }

    public void Update(string? name, decimal? price, int? stock)
    {
        Name = name ?? Name;
        Price = price ?? Price;
        Stock = stock ?? Stock;
    }

    public bool StockIsNotAvailable(int quantity)
        => quantity.IsNotInRange(0, Stock);

    public void DecreaseStock(int quantity)
        => Stock -= quantity;
}