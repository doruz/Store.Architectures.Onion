using Store.Business.Products;

namespace Store.Business.ShoppingCarts;

public record ShoppingCartModel
{
    public IReadOnlyList<CartLineModel> Lines { get; init; } = [];
}

public record CartLineModel
{
    public string ProductId { get; init; }

    public string ProductName { get; init; }

    public PriceModel ProductPrice { get; set; }

    public int Quantity { get; init; }

    public PriceModel TotalPrice { get; set; }
}