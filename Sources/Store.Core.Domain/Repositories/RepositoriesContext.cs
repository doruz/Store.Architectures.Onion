namespace Store.Core.Domain.Repositories;

public sealed class RepositoriesContext(IProductsRepository products, IShoppingCartsRepository shoppingCarts)
{
    public IProductsRepository Products { get; } = products;

    public IShoppingCartsRepository ShoppingCarts { get; } = shoppingCarts;
}