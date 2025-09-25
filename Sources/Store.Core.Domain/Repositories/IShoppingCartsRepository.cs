using Store.Core.Domain.Entities;

namespace Store.Core.Domain.Repositories;

public interface IShoppingCartsRepository
{
    Task<ShoppingCart?> FindAsync(string accountId);
    
    async Task<ShoppingCart> FindOrEmptyAsync(string accountId)
        => await FindAsync(accountId) ?? ShoppingCart.CreateEmpty(accountId);

    Task UpdateAsync(ShoppingCart cart);

    Task DeleteAsync(string accountId); 
}