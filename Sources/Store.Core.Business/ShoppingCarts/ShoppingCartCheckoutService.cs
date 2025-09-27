using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartCheckoutService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task CheckoutCurrentAccountCart()
    {
        var accountOrder = CreateOrder(await GetShoppingCartItems());
        if (accountOrder.IsNotEmpty())
        {
            await repositories.Orders.SaveOrderAsync(accountOrder);
            await repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);
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

    private Order CreateOrder(List<(ShoppingCartLine CartLine, Product Product)> items) => new()
    {
        AccountId = currentAccount.Id,

        Lines = items
            .Select(i => CreateOrderLine(i.CartLine, i.Product))
            .ToList()
    };

    private OrderLine CreateOrderLine(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsTrue(cartLine.ProductId.IsEqualTo(product.Id));

        return new OrderLine
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = product.Price,

            Quantity = cartLine.Quantity
        };
    }
}