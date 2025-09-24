using Store.Business.Products;
using Store.Core.Domain;

namespace Store.Business.ShoppingCarts;

public record ShoppingCartModel
{
    public IReadOnlyList<CartLineModel> Lines { get; init; } = [];
}

public record CartLineModel
{
    public string ProductId { get; init; }

    public string ProductName { get; init; }

    public Price ProductPrice { get; set; }

    public int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}