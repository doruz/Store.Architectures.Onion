using Store.Core.Business.Products;
using Store.Core.Domain;

namespace Store.Core.Business.ShoppingCarts;

public record ShoppingCartModel
{
    public IReadOnlyList<ShoppingCartLineModel> Lines { get; init; } = [];

    public Price TotalPrice => Lines.Sum(line => line.TotalPrice);
}

public record ShoppingCartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; set; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}