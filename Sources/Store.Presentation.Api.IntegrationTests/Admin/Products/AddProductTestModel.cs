using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests.Admin.Products;

public record AddProductTestModel(string Name, decimal Price, int Stock)
{
    internal static AddProductTestModel CreateRandomDetails() => new
    (
        Name: TestRandom.Strings.Generate(1, 100),
        Price: TestRandom.Numbers.Generate(0, int.MaxValue),
        Stock: TestRandom.Numbers.Generate(0, int.MaxValue)
    );

    internal dynamic GetExpectedDetails() => new
    {
        Name,
        Price = new Price(Price),
        Stock
    };

    internal dynamic GetExpectedDetails(string id) => new
    {
        Id = id,
        Name,
        Price = new Price(Price),
        Stock
    };
}