using EnsureThat;

namespace Store.Core.Domain.Entities;

public sealed class ShoppingCart : BaseEntity
{
    public List<ShoppingCartLine> Lines { get; init; } = [];

    public static ShoppingCart CreateEmpty(string customerId) => new() { Id = customerId };

    public bool IsEmpty() => Lines.All(line => line.Quantity == 0);

    public void UpdateOrRemoveLines(params ShoppingCartLine[] lines)
        => lines.Merge().ForEach(UpdateOrRemoveLine);

    public void UpdateOrRemoveLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        RemoveLine(line.ProductId);

        if (line.Quantity > 0)
        {
            AddLine(line);
        }
    }

    private void RemoveLine(string productId)
    {
        EnsureArg.IsNotNullOrEmpty(productId, nameof(productId));

        Lines.RemoveAll(line => line.ProductId.IsEqualTo(productId));
    }

    private void AddLine(ShoppingCartLine line)
    {
        EnsureArg.IsNotNull(line, nameof(line));

        Lines.Add(line);
    }
}