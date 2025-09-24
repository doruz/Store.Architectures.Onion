using EnsureThat;
using Store.Business.Products;
using Store.Core.Domain;
using Store.Core.Repositories;
using Store.Shared.Extensions;

namespace Store.Business.ShoppingCarts;

public sealed class ShoppingCartsService(RepositoriesContext repositories)
{
    private const string DummyAccountId = "1";

    public async Task<ShoppingCartModel> GetCurrentAccountCart()
    {
        var shoppingCart = await repositories.ShoppingCarts.FindAsync(DummyAccountId)
                           ?? ShoppingCart.CreateEmpty(DummyAccountId);

        return new ShoppingCartModel
        {
            Lines = await ToCartLinesModel(shoppingCart.Lines)
        };
    }

    private async Task<List<CartLineModel>> ToCartLinesModel(IEnumerable<CartLine> cartLines)
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

    private static CartLineModel ToCartLineModel(CartLine cartLine, Product product)
    {
        EnsureArg.IsNotNull(cartLine, nameof(cartLine));
        EnsureArg.IsNotNull(product, nameof(product));

        return new CartLineModel
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductPrice = PriceModel.Create(product.Price),
            
            Quantity = cartLine.Quantity,
            TotalPrice = PriceModel.Create(product.Price * cartLine.Quantity)
        };
    }
}