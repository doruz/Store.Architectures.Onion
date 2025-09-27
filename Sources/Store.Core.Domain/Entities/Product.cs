namespace Store.Core.Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }

    public required Price Price { get; set; }

    // TODO: extend with StockQuantity & Category
}