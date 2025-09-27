using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

internal static class OrdersMapper
{
    public static OrderSummaryModel ToOrderSummaryModel(this Order order) => new()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToOrderedAt(),

        TotalProducts = order.TotalProducts,
        TotalPrice = order.TotalPrice
    };

    public static OrderDetailedModel ToOrderDetailedModel(this Order order) => new ()
    {
        Id = order.Id,
        OrderedAt = order.CreatedAt.ToOrderedAt(),

        TotalProducts = order.TotalProducts,
        TotalPrice = order.TotalPrice,

        Lines = order.Lines.Select(ToOrderLineModel).ToList()
    };

    private static OrderDetailedLineModel ToOrderLineModel(this OrderLine product) => new()
    {
        ProductId = product.ProductId,
        ProductName = product.ProductName,
        ProductPrice = product.ProductPrice,
        Quantity = product.Quantity
    };

    private static ValueLabel<DateTime> ToOrderedAt(this DateTime orderedAt)
        => new(orderedAt, orderedAt.ToDateTimeString());
}