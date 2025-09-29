using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

public record OrderDetailedModel
{
    public required string Id { get; init; }

    public required ValueLabel<DateTime> OrderedAt { get; init; }

    public required int TotalProducts { get; init; }
    public required Price TotalPrice { get; init; }

    public required IReadOnlyList<OrderDetailedLineModel> Lines { get; init; } = [];
}

public record OrderDetailedLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}