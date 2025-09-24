namespace Store.Core.Repositories;

public interface IShoppingCartsRepository
{
    Task<ShoppingCart?> FindAsync(string accountId);
}