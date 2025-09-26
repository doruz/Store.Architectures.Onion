using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.InMemory;

internal sealed class InMemoryShoppingCartsRepository(InMemoryCollections collections) : IShoppingCartsRepository
{
    public async Task<ShoppingCart?> FindAsync(string accountId)
        => collections.ShoppingCarts.Find(cart => cart.Id.IsEqualTo(accountId));

    public async Task AddAsync(ShoppingCart cart)
        => collections.ShoppingCarts.Add(EnsureArg.IsNotNull(cart, nameof(cart)));

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        await DeleteAsync(cart.Id);
        await AddAsync(cart);
    }

    public async Task DeleteAsync(string accountId)
    {
        collections.ShoppingCarts.RemoveAll(cart => cart.Id.IsEqualTo(accountId));
    }
}