namespace Store.Core.Repositories;

public interface IShoppingCartsRepository
{
    Task<ShoppingCart?> FindAsync(string accountId);

    async Task<ShoppingCart> FindOrEmptyAsync(string accountId)
        => await FindAsync(accountId) ?? ShoppingCart.CreateEmpty(accountId);
}