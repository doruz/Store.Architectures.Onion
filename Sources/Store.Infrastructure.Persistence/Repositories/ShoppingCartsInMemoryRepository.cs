using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;

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
                new ShoppingCartLine("9952a5da-9945-11f0-9ae5-2f38b5112d0d") { Quantity = 3 },
                new ShoppingCartLine("dc552437-0b7d-455a-afa0-e4949142a4ad") { Quantity = 2 },
                new ShoppingCartLine("non-existing") { Quantity = 2 },
            ]
        };
    }
}