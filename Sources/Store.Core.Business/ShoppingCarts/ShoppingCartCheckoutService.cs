using Store.Core.Business.Orders;
using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartCheckoutService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task<OrderSummaryModel> CheckoutCurrentAccountCart()
    {
        var shoppingCartItems = await GetShoppingCartItems();

        shoppingCartItems.ForEach(l => l.Product.EnsureStockIsAvailable(l.CartLine.Quantity));

        var orderLines = shoppingCartItems
            .Select(item => OrderLine.Create(item.CartLine, item.Product))
            .ToList();

        var accountOrder = Order.Create(currentAccount.Id, orderLines);
        await repositories.Orders.SaveOrderAsync(accountOrder);
        await repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);

        await UpdateProductsStock(shoppingCartItems);

        return accountOrder.ToOrderSummaryModel();
    }

    private async Task<List<(ShoppingCartLine CartLine, Product Product)>> GetShoppingCartItems()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        shoppingCart.EnsureIsNotEmpty();

        return await shoppingCart.Lines
            .Where(cartLine => cartLine.Quantity > 0)
            .Select(async cartLine =>
            (
                cartLine,
                (await repositories.Products.FindAsync(cartLine.ProductId))!
            ))
            .ToListAsync();
    }

    private async Task UpdateProductsStock(List<(ShoppingCartLine CartLine, Product Product)> items)
    {
        foreach (var item in items)
        {
            item.Product.DecreaseStock(item.CartLine.Quantity);
            await repositories.Products.UpdateAsync(item.Product);
        }
    }
}