using Store.Core.Domain.Entities;

namespace Store.Core.Business.Products;

public record ProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; } 

    public required Price Price { get; init; }
}