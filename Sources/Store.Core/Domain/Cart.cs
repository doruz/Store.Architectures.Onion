using EnsureThat;
using Store.Core.Domain;

public sealed class Cart : BaseEntity
{
    public List<CartLine> CartLines { get; init; } = [];

    public void AddLine(string productId, int quantity)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));
        EnsureArg.IsGte(quantity, 1, nameof(quantity));

        var cartLine = CartLines.FirstOrDefault(l => l.ProductId == productId);
        if (cartLine is null)
        {
            CartLines.Add(new CartLine(productId) { Quantity = quantity });
        }
        else
        {
            cartLine.Quantity = quantity;
        }
    }

    public void RemoveLine(string productId)
    {
        CartLines.RemoveAll(line => line.ProductId == productId);
    }
}

public sealed record CartLine(string ProductId)
{
    public int Quantity { get; set; } = 1;
}