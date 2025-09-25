using EnsureThat;
using Store.Core.Business.Products;
using Store.Core.Domain;
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

    private static ShoppingCartLineModel ToCartLineModel(ShoppingCartLine cartLine, Product product)
    {
        EnsureArg.IsNotNull(cartLine, nameof(cartLine));
        EnsureArg.IsNotNull(product, nameof(product));

        return new ShoppingCartLineModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = product.Price,
            
            Quantity = cartLine.Quantity,
        };
    }
}