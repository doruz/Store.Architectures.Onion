using Store.Business.Products;
using Store.Core.Domain;

namespace Store.Business.ShoppingCarts;

public record ShoppingCartModel
{
    public IReadOnlyList<CartLineModel> Lines { get; init; } = [];

    public Price TotalPrice => Lines.Sum(line => line.TotalPrice);
}

public record CartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; set; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}