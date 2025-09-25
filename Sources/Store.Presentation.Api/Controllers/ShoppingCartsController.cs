using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.ShoppingCarts;

[ApiRoute("shopping-carts/current")]
public class ShoppingCartsController(ShoppingCartsService shoppingCarts) : BaseApiController
{
    /// <summary>
    /// Get current cart of authenticated account.
    /// If there is no cart created, create an empty one.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCurrentCart() 
        => Ok(await shoppingCarts.GetCurrentAccountCart());

    /*
     * PUT: to update entire shopping cart
     * DELETE: to remove entire shopping cart
     * PUT: to update one line, if quantity is zero, to be removed
     * DELETE: to remove entire shopping cart line
     */
}