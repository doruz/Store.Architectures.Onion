using Store.Core.Domain.Entities;
using Store.Core.Shared;

namespace Store.Core.Business.Orders;

internal static class OrdersMapper
{
    public static OrderSummaryModel ToSummaryModel(this Order order) => new()
    {
        Id = order.Id,
        OrderedAt = new (order.CreatedAt, order.CreatedAt.ToDateTimeString()),
        TotalProducts = order.Products.Count,
        TotalPrice = order.TotalPrice
    };

    public static OrderDetailedModel ToDetailedModel(this Order order) => new ()
    {
        Id = order.Id,
        OrderedAt = new(order.CreatedAt, order.CreatedAt.ToDateTimeString()),
        TotalPrice = order.TotalPrice,

        Products = order.Products.Select(ToOrderDetailedProductModel).ToList()
    };

    private static OrderDetailedProductModel ToOrderDetailedProductModel(this OrderProductDetails product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Price = product.Price,
        Quantity = product.Quantity
    };
}