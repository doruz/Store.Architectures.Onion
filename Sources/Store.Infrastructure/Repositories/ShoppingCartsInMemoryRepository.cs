using Store.Core.Repositories;

namespace Store.Infrastructure.Repositories;

internal sealed class ShoppingCartsInMemoryRepository : IShoppingCartsRepository
{
    public async Task<ShoppingCart?> FindAsync(string accountId)
    {
        return new ShoppingCart
        {
            Id = accountId,
            Lines =
            [
                new CartLine("9952a5da-9945-11f0-9ae5-2f38b5112d0d") { Quantity = 3 },
                new CartLine("9952a5da-9945-11f0-9ae5-2f38b5112d0d") { Quantity = 2 },
                new CartLine("non-existing") { Quantity = 2 },
            ]
        };
    }
}