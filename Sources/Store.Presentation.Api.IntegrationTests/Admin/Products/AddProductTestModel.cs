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
        Price = new { Value = Price, Currency = "€", Display = $"€{Price:F2}" },
        Stock
    };

    internal dynamic GetExpectedDetails(string id) => new
    {
        Id = id,
        Name,
        Price = new { Value = Price, Currency = "€", Display = $"€{Price:F2}" },
        Stock
    };
}

public record EditProductTestModel
{
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public int? Stock { get; init; }
}