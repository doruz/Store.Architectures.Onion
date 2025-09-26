using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public record ProductReadModel
{
    public string Id { get; init; }

    public string Name { get; init; } 

    public Price Price { get; init; }
}