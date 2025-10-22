using Store.Core.Domain.Entities;

namespace Store.Core.Business.Orders;

public record OrderDetailedModel : OrderSummaryModel
{
    public required IReadOnlyList<OrderDetailedLineModel> Lines { get; init; } = [];
}

public record OrderDetailedLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public PriceModel TotalPrice => PriceModel.Create(ProductPrice * Quantity);
}