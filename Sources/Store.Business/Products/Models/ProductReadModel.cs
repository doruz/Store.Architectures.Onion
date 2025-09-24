namespace Store.Business.Products;

public record ProductReadModel
{
    public string Id { get; init; }

    public string Name { get; init; } 

    public PriceModel Price { get; init; }
}