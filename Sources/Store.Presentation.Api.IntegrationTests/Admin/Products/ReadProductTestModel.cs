using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

internal record ReadProductTestModel
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public int Stock { get; init; }
    public required PriceTestModel Price { get; init; }

    public static ReadProductTestModel Create(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Stock = product.Stock,
        Price = new PriceTestModel(product.Price.Value)
    };
}

public record PriceTestModel(decimal Value)
{
    public string Currency => "€";
    public string Display => $"€{Value:F2}";
}