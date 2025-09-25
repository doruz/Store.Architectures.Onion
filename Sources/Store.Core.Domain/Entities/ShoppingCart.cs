using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed class ShoppingCart : BaseEntity
{
    public List<ShoppingCartLine> Lines { get; init; } = [];

    public void AddLine(string productId, int quantity)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));
        EnsureArg.IsGte(quantity, 1, nameof(quantity));

        var cartLine = Lines.FirstOrDefault(l => l.ProductId == productId);
        if (cartLine is null)
        {
            Lines.Add(new ShoppingCartLine(productId) { Quantity = quantity });
        }
        else
        {
            cartLine.Quantity = quantity;
        }
    }

    public void RemoveLine(string productId)
    {
        Lines.RemoveAll(line => line.ProductId == productId);
    }

    public static ShoppingCart CreateEmpty(string accountId) => new() { Id = accountId };
}

public sealed record ShoppingCartLine(string ProductId)
{
    public int Quantity { get; set; } = 1;
}