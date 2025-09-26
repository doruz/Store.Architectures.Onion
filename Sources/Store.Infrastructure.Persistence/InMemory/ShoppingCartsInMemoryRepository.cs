using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence;

internal sealed class ShoppingCartsInMemoryRepository : IShoppingCartsRepository
{
    private readonly List<ShoppingCart> _shoppingCarts =
    [
        new ShoppingCart
        {
            Id = "32b1ed6f-f551-45ae-b6d3-695b86aacad8",
            Lines =
            [
                new ShoppingCartLine("9952a5da-9945-11f0-9ae5-2f38b5112d0d", 3),
                new ShoppingCartLine("dc552437-0b7d-455a-afa0-e4949142a4ad", 2),
                new ShoppingCartLine("non-existing", 2)
            ]
        }
    ];

    public async Task<ShoppingCart?> FindAsync(string accountId)
        => _shoppingCarts.Find(cart => cart.Id.IsEqualTo(accountId));

    public async Task AddAsync(ShoppingCart cart)
        => _shoppingCarts.Add(EnsureArg.IsNotNull(cart, nameof(cart)));

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        await DeleteAsync(cart.Id);
        await AddAsync(cart);
    }

    public async Task DeleteAsync(string accountId)
    {
        _shoppingCarts.RemoveAll(cart => cart.Id.IsEqualTo(accountId));
    }
}