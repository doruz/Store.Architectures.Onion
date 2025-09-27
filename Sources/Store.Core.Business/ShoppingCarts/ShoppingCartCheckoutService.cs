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
        var orderLines = await CreateOrderLinesFromShoppingCart();
        if (orderLines.IsNotEmpty())
        {
            var accountOrder = Order.Create(currentAccount.Id, orderLines);
            await repositories.Orders.SaveOrderAsync(accountOrder);

            await repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);
        }
    }

    private async Task<List<OrderLine>> CreateOrderLinesFromShoppingCart()
    {
        var shoppingCartItems = await GetShoppingCartItems();

        return shoppingCartItems
            .Select(item => OrderLine.Create(item.CartLine, item.Product))
            .ToList();
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
}