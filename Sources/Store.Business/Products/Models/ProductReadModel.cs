using Store.Core.Domain;

namespace Store.Business.Products;

public record ProductReadModel
{
    public string Id { get; init; }

    public string Name { get; init; } 

    public Price Price { get; init; }
}