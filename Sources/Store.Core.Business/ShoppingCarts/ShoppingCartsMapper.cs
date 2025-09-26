using EnsureThat;
using Store.Core.Domain.Entities;

namespace Store.Core.Business.ShoppingCarts;

internal static class ShoppingCartsMapper
{
    public static ShoppingCartLineReadModel ToShoppingCartLineReadModel(this ShoppingCartLine cartLine, Product product)
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

    public static ShoppingCartLine ToShoppingCartLine(this ShoppingCartLineWriteModel model)
        => new ShoppingCartLine(model.ProductId, model.Quantity);
}