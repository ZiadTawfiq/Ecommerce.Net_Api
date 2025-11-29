using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Product")]
    public class ProductsController(IProductService productService,ILogger<ProductsController>logger):ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateProudct([FromBody]ProductDto productDto) 
        {
            await productService.AddAsync(productDto);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            await productService.DeleteAsync(id);
            return NoContent();
        }
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
                return NotFound("Product not found");

            return Ok(product);
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllAsync();
            return Ok(products);
        }
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto productDto)
        {
            await productService.UpdateAsync(productDto);
            return Ok("Product updated successfully");
        }
    }
}
