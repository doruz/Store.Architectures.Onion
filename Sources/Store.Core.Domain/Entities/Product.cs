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
}