using Microsoft.AspNetCore.Mvc;
using Store.Core.Business.Products;

[ApiRoute("products")]
public sealed class ProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts() 
        => Ok(await products.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> FindProduct([FromRoute] string id) 
        => OkOrNotFound(await products.FindProductAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductWriteModel model)
    {
        var newProduct = await products.Create(model);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] ProductWriteModel model)
        => NoContentOrNotFound(await products.Update(id, model));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        => NoContentOrNotFound(await products.Delete(id));
}