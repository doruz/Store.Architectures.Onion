using Store.Core.Business.Products;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartsService(RepositoriesContext repositories, ICurrentCustomer currentCustomer)
{
    public async Task<ShoppingCartModel> GetCurrentCustomerCart()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        return new ShoppingCartModel
        {
            Lines = await ToCartLinesModel(shoppingCart.Lines)
        };
    }

    private async Task<List<ShoppingCartLineModel>> ToCartLinesModel(IEnumerable<ShoppingCartLine> cartLines)
    {
        var lines = await cartLines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await repositories.Products.FindAsync(cartLine.ProductId)
            })
            .ToListAsync();

        return lines
            .Where(x => x.Product != null)
            .Select(x => x.CartLine.ToShoppingCartLineModel(x.Product!))
            .ToList();
    }

    public Task ClearCurrentCustomerCart()
        => repositories.ShoppingCarts.DeleteAsync(currentCustomer.Id);

    public async Task UpdateCurrentCustomerCart(params EditShoppingCartLineModel[] lines)
    {
        if (lines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentCustomer.Id);

        shoppingCart.UpdateOrRemoveLines(await GetValidLines(lines));
       
        await repositories.ShoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<EditShoppingCartLineModel> cartLines)
    {
        var lines = await cartLines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                Product = await repositories.Products.FindAsync(cartLine.ProductId)
            })
            .ToListAsync();

        lines.ForEach(l =>
        {
            l.Product
                .EnsureIsNotNull(l.CartLine.ProductId)
                .EnsureStockIsAvailable(l.CartLine.Quantity);
        });

        return lines
            .Select(l => l.CartLine.ToShoppingCartLine())
            .ToArray();
    }
}