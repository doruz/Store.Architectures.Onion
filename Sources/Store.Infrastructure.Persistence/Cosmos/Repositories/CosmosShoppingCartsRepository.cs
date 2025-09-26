using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

namespace Store.Infrastructure.Persistence.Cosmos;

internal sealed class CosmosShoppingCartsRepository(CosmosDatabaseContainers containers) : IShoppingCartsRepository
{
    public Task<ShoppingCart?> FindAsync(string accountId)
        => containers.ShoppingCarts.FindAsync<ShoppingCart>(accountId, accountId.ToPartitionKey());

    public async Task AddOrUpdateAsync(ShoppingCart cart)
    {
        EnsureArg.IsNotNull(cart, nameof(cart));

        await containers.ShoppingCarts.UpsertItemAsync(cart);
    }

    public Task DeleteAsync(string accountId)
        => containers.ShoppingCarts.DeleteAsync<ShoppingCart>(accountId, accountId.ToPartitionKey());
}