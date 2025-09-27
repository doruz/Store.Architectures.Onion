using Store.Core.Domain.Entities;

namespace Store.Core.Business.Orders;

public record OrderDetailedLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}