using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;

[ApiRoute("products")]
public sealed class ProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts() 
        => Ok(await products.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] string id) 
        => OkOrNotFound(await products.FindProductAsync(id));

    [HttpPost]
    [ProducesResponseType<ProductModel>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddProduct([FromBody] NewProductModel model)
    {
        var newProduct = await products.CreateAsync(model);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] EditProductModel model)
    {
        await products.UpdateAsync(id, model);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<AppErrorModel>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        await products.DeleteAsync(id);

        return NoContent();
    }
}