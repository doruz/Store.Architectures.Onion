namespace Store.Core.Domain.Entities;

public class Product : BaseEntity
{
    public Product()
    {
        // TODO: to be removed
    }

    public Product(string id, string name, int stock, Price price)
    {
        Id = id;
        Name = name;
        Stock = stock;
        Price = price;
    }

    public string Name { get; set; }

    public Price Price { get; set; } = 0;

    public int Stock { get; set; }

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