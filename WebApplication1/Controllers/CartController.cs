using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Cart")]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost]
        [Route("AddProductToCart/{userId}")]
        public async Task<IActionResult> AddProduct([FromRoute]int userId, [FromBody]ProductToCartDto Dto)
        {
            try
            {
                await cartService.AddProductToCart(userId, Dto);
                return Ok(new { message = "Product added to cart successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }  
        }
        [HttpPut]
        [Route("Decrease/{productId}/{cartId}")]
        public async Task<IActionResult> DecreaseElement([FromRoute]int productId , [FromRoute] int cartId)
        {
            try
            {
                await cartService.DecreaseCartItem(productId, cartId);
                return Ok(new{Message = $"Decrease {productId} by 1"}); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }
        [HttpDelete]
        [Route("DeleteItem/{prodId}/{cartId}")]
        public async Task<IActionResult> DeleteItem([FromRoute]int prodId , [FromRoute] int cartId)
        {
            try
            {
                await cartService.DeleteItem(prodId, cartId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("Clear/{id}")]
        public async Task<IActionResult> ClearCart([FromRoute]int id)
        {
            try
            {
                await cartService.ClearCart(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        } 
       

    }
}
