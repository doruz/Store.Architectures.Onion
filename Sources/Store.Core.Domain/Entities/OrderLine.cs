using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record OrderLine
{
    public required string ProductId { get; init; }

    public required string ProductName { get; init; }

    public required Price ProductPrice { get; init; }

    public required int Quantity { get; init; }

    public Price TotalPrice => ProductPrice * Quantity;

    private OrderLine() { }

    public static OrderLine Create(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsTrue(cartLine.ProductId.IsEqualTo(product.Id));
        EnsureArg.IsInRange(cartLine.Quantity, 0, product.Stock);

        return new OrderLine
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = product.Price,

            Quantity = cartLine.Quantity
        };
    }
}