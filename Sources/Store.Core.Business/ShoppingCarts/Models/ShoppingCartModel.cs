using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

public record ShoppingCartModel
{
    public IReadOnlyList<ShoppingCartLineModel> Lines { get; init; } = [];

    public PriceModel TotalPrice => PriceModel.Create(Lines.Select(line => line.TotalPrice).Sum());
}

public record ShoppingCartLineModel
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; set; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;
}