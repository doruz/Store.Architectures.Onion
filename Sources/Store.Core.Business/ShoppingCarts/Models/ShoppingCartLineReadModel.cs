using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

public record ShoppingCartLineReadModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; set; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}