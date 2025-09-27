using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

public record OrderSummaryModel
{
    public required string Id { get; init; }

    public required ValueLabel<DateTime> OrderedAt { get; init; }

    public required int TotalProducts { get; init; }
    public required Price TotalPrice { get; init; }
}