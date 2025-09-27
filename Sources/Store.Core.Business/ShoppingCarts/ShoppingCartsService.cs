using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartsService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task<ShoppingCartModel> GetCurrentAccountCart()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        return new ShoppingCartModel
        {
            Lines = await ToCartLinesModel(shoppingCart.Lines)
        };
    }

    private async Task<List<ShoppingCartLineModel>> ToCartLinesModel(IEnumerable<ShoppingCartLine> cartLines)
    {
        var lines = await cartLines
            .Where(cartLine => cartLine.Quantity > 0)
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

    public Task ClearCurrentAccountCart()
        => repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);

    public async Task UpdateCurrentAccountCart(params ShoppingCartLineEditModel[] lines)
    {
        var validLines = await GetValidLines(lines);
        if (validLines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        shoppingCart.UpdateOrRemoveLines(validLines);
       
        await repositories.ShoppingCarts.AddOrUpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<ShoppingCartLineEditModel> cartLines)
    {
        var lines = await cartLines
            .Select(async cartLine => new
            {
                CartLine = cartLine,
                IsValidProduct = await repositories.Products.ExistsAsync(cartLine.ProductId)
            })
            .ToListAsync();

        return lines
            .Where(x => x.IsValidProduct)
            .Select(x => x.CartLine.ToShoppingCartLine())
            .ToArray();
    }
}