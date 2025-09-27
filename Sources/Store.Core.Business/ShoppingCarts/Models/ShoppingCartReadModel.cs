using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

public record ShoppingCartReadModel
{
    public IReadOnlyList<ShoppingCartLineReadModel> Lines { get; init; } = [];

    public Price TotalPrice => Lines.Select(line => line.TotalPrice).Sum();
}