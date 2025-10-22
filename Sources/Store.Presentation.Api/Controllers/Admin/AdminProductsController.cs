using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;
using Store.Core.Business.Shared;

[ApiRoute("admins/products")]
public sealed class AdminProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<ProductModel>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProducts() 
        => Ok(await products.GetAll());

    [HttpGet("{id}")]
    [ProducesResponseType<ProductModel>(StatusCodes.Status200OK)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> FindProduct([FromRoute] string id) 
        => Ok(await products.FindProductAsync(id));

    [HttpPost]
    [ProducesResponseType<ProductModel>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddProduct([FromBody] NewProductModel model)
    {
        var newProduct = await products.AddAsync(model);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] EditProductModel model)
    {
        await products.UpdateAsync(id, model);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<BusinessError>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
    {
        await products.DeleteAsync(id);

        return NoContent();
    }
}