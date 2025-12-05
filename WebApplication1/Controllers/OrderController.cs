using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("Order")]
    public class OrderController(IOrderService orderService):ControllerBase
    {
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody]OrderDto orderDto)
        {
            try
            {
                await orderService.CreateOrder(orderDto);
                return Ok("order has been Created successfully."); 
            }
            catch(Exception ex )
            {
                return  BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        [Route("Cancel/{orderId}")]
        public async Task<IActionResult>CancelOrder([FromRoute] int orderId)
        {
            try
            {
                await orderService.CancelOrder(orderId);
                return Ok("Cancelled Succefully"); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
