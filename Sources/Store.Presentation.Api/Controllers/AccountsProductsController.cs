using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;

[ApiRoute("accounts/current/products")]
public sealed class AccountsProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAvailableProduct()
        => Ok(await products.GetAllAvailable());

    [HttpGet("{id}")]
    public async Task<IActionResult> FindProduct([FromRoute] string id)
        => OkOrNotFound(await products.FindProductAsync(id));
}