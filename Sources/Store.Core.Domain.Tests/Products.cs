using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Tests;

internal static class Products
{
    public static readonly Product First = new()
    {
        Id = "1",
        Name = "First",
        Price = 0.5m,
        Stock = 10
    };

    public static readonly Product Second = new()
    {
        Id = "2",
        Name = "Second",
        Price = 0.99m,
        Stock = 10
    };
}