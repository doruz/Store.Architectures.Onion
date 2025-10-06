using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests;

internal static class ProductsTestData
{
    public const string UnknownId = "Unknown";
    public const string BananasId = "9b5055cf-6cd0-4086-8d01-6e1582a7fb0a";
    public const string ApplesId = "b4f256a5-f65f-4811-a0d4-10d1fbba5f25";
    public const string OrangesId = "5c6a5841-1e91-4ff1-96d7-091affe74dc5";

    public static IReadOnlyList<Product> All =
    [
        new Product
        (
            id: BananasId,
            name: "Bananas",
            stock: 5,
            price: 0.99m
        ),

        new Product
        (
            id: ApplesId,
            name: "Apples",
            stock: 10,
            price: 0.75m
        ),

        new Product
        (
            id: OrangesId,
            name: "Oranges",
            stock: 0,
            price: 0.75m
        )
    ];
}