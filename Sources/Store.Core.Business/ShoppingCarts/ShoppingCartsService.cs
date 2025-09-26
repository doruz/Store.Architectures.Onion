using EnsureThat;
using Store.Core.Domain.Entities;
using Store.Core.Domain.Repositories;
using Store.Core.Shared;

namespace Store.Core.Business.ShoppingCarts;

public sealed class ShoppingCartsService(RepositoriesContext repositories, ICurrentAccount currentAccount)
{
    public async Task<ShoppingCartReadModel> GetCurrentAccountCart()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        return new ShoppingCartReadModel
        {
            Lines = await ToCartLinesModel(shoppingCart.Lines)
        };
    }

    private async Task<List<ShoppingCartLineReadModel>> ToCartLinesModel(IEnumerable<ShoppingCartLine> cartLines)
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
            .Select(x => ToCartLineModel(x.CartLine, x.Product!))
            .ToList();
    }

    private static ShoppingCartLineReadModel ToCartLineModel(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsNotNull(cartLine, nameof(cartLine));
        EnsureArg.IsNotNull(product, nameof(product));

        return new ShoppingCartLineReadModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = product.Price,
            
            Quantity = cartLine.Quantity,
        };
    }


    // DELETE CART
    public Task ClearCurrentAccountCart()
        => repositories.ShoppingCarts.DeleteAsync(currentAccount.Id);

    // UPDATE CART LINES
    public async Task UpdateCurrentAccountCartAsync(params ShoppingCartLineWriteModel[] lines)
    {
        var validLines = await GetValidLines(lines);
        if (validLines.IsEmpty())
        {
            return;
        }

        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        shoppingCart.UpdateOrRemoveLines(validLines);
       
        await repositories.ShoppingCarts.UpdateAsync(shoppingCart);
    }

    private async Task<ShoppingCartLine[]> GetValidLines(IEnumerable<ShoppingCartLineWriteModel> cartLines)
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
            .Select(x => new ShoppingCartLine(x.CartLine.ProductId, x.CartLine.Quantity))
            .ToArray();
    }

    // DELETE cart product
    public async Task RemoveProductFromCurrentAccountCartAsync(string productId)
    {
        var shoppingCart = await repositories.ShoppingCarts.FindOrEmptyAsync(currentAccount.Id);

        shoppingCart.RemoveLine(productId);

        await repositories.ShoppingCarts.UpdateAsync(shoppingCart);
    }
}