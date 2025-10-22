using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("customers/current/products")]
public sealed class CustomersProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAvailableProducts()
        => Ok(await products.GetAllAvailable());

    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] string id)
        => Ok(await products.FindProductAsync(id));
}