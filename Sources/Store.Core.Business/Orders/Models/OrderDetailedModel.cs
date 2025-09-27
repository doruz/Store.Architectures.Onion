using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

public record OrderDetailedModel
{
    public required string Id { get; init; }

    public required ValueLabel<DateTime> OrderedAt { get; init; }

    public required Price TotalPrice { get; init; }

    public required IReadOnlyList<OrderDetailedProductModel> Products { get; init; } = [];
}