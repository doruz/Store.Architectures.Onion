using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests;

internal static class TestProducts
{
    public const string UnknownId = "Unknown";

    public static readonly Product Bananas = new()
    {
        Id = "b4f256a5-f65f-4811-a0d4-10d1fbba5f25",
        Name = "Bananas",
        Stock = 10,
        Price = 0.75m
    };

    public static readonly Product Apples = new()
    {
        Id = "9b5055cf-6cd0-4086-8d01-6e1582a7fb0a",
        Name = "Apples",
        Stock = 5,
        Price = 0.99m
    };

    public static readonly Product Oranges = new()
    {
        Id = "5c6a5841-1e91-4ff1-96d7-091affe74dc5",
        Name = "Oranges",
        Stock = 0,
        Price = 0.55m
    };

    public static IReadOnlyList<Product> All =
    [
        Apples,
        Bananas,
        Oranges
    ];
}