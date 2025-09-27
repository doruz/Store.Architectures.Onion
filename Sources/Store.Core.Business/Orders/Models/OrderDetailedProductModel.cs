using Store.Core.Domain.Entities;

namespace Store.Core.Business.Orders;

public record OrderDetailedProductModel
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required int Quantity { get; init; }

    public required Price Price { get; init; }

    public Price TotalPrice => Price * Quantity;
}