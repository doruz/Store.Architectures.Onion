using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; } 

    public required PriceModel Price { get; init; }

    public required int Stock { get; init; }
}

public record PriceModel(decimal Value, string Currency, string Display)
{
    public static PriceModel Create(Price price) => new(price.Value, price.Currency, price.ToString());
};