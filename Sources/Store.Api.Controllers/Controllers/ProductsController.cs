using Microsoft.AspNetCore.Mvc;
using Store.Business.Products;

[ApiRoute("products")]
public class ProductsController(ProductsService products) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts() 
        => Ok(await products.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> FindProduct([FromRoute] string id) 
        => OkOrNotFound(await products.FindProductAsync(id));

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductWriteModel model)
    {
        var newProduct = await products.CreateAsync(model);

        return CreatedAtAction(nameof(FindProduct), new { newProduct.Id }, newProduct);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] string id, [FromBody] ProductWriteModel model)
        => NoContentOrNotFound(await products.UpdateAsync(id, model));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] string id)
        => NoContentOrNotFound(await products.DeleteAsync(id));
}