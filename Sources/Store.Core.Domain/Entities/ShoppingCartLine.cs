using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed record ShoppingCartLine
{
    public string ProductId { get; }
    public int Quantity { get; }

    public ShoppingCartLine(string productId, int quantity)
    {
        ProductId = EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));
        Quantity = EnsureArg.IsGte(quantity, 0, nameof(quantity));
    }
}