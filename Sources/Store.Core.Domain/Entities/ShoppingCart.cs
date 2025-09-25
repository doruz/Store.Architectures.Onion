using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed class ShoppingCart : BaseEntity
{
    public List<ShoppingCartLine> Lines { get; init; } = [];

    public static ShoppingCart CreateEmpty(string accountId) => new() { Id = accountId };


    public void UpdateOrRemoveLines(params ShoppingCartLine[] lines)
        => lines.ForEach(UpdateOrRemoveLine);

    public void UpdateOrRemoveLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        RemoveLine(line.ProductId);

        if (line.Quantity > 0)
        {
            AddLine(line);
        }
    }

    private void AddLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        Lines.Add(line);
    }

    public void RemoveLine(string productId)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));

        Lines.RemoveAll(line => line.ProductId.IsEqualTo(productId));
    }
}