using Store.Core.Domain.Entities;

namespace Store.Presentation.Api.IntegrationTests;

internal static class TestProducts
{
    public const string UnknownId = "Unknown";

    public static readonly Product Bananas = new
    (
        id: "b4f256a5-f65f-4811-a0d4-10d1fbba5f25",
        name: "Bananas",
        stock: 10,
        price: 0.75m
    );

    public static readonly Product Apples = new
    (
        id: "9b5055cf-6cd0-4086-8d01-6e1582a7fb0a",
        name: "Apples",
        stock: 5,
        price: 0.99m
    );

    public static readonly Product Oranges = new
    (
        id: "5c6a5841-1e91-4ff1-96d7-091affe74dc5",
        name: "Oranges",
        stock: 0,
        price: 0.55m
    );

    public static IReadOnlyList<Product> All =
    [
        Apples,
        Bananas,
        Oranges
    ];
}