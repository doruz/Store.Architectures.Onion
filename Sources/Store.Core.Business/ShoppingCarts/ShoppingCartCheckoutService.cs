using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

/*
 * TODO: to ensure stock is available at checkout
 */
public sealed class ShoppingCartCheckoutService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task CheckoutCurrentAccountCart()
    {
        var shoppingCartItems = await GetShoppingCartItems();

        var orderLines = shoppingCartItems
            .Select(item => OrderLine.Create(item.CartLine, item.Product))
            .ToList();

        if (orderLines.IsNotEmpty())
        {
            var accountOrder = Order.Create(currentAccount.Id, orderLines);
            await repositories.Orders.SaveOrderAsync(accountOrder);
            await repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);

            await UpdateProductsStock(shoppingCartItems);
        }
    }

    private async Task<List<(ShoppingCartLine CartLine, Product Product)>> GetShoppingCartItems()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        return await shoppingCart.Lines
            .Where(cartLine => cartLine.Quantity > 0)
            .Select(async cartLine =>
            (
                cartLine,
                (await repositories.Products.FindAsync(cartLine.ProductId))!
            ))
            .ToListAsync();
    }

    // TODO: it's better to let the stock on minus, instead of throwing an exception to the client and to go on an incosisten state
    // TODO: in this case it's better to notify an admin
    private async Task UpdateProductsStock(List<(ShoppingCartLine CartLine, Product Product)> items)
    {
        foreach (var item in items)
        {
            item.Product.DecreaseStock(item.CartLine.Quantity);
            await repositories.Products.UpdateAsync(item.Product);
        }
    }
}