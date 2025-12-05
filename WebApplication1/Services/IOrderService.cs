using WebApplication1.DTOs;

namespace WebApplication1.Repositories
{
    public interface IOrderService
    {
        Task CreateOrder(OrderDto orderDto);
        Task CancelOrder(int OrderId); 
    }
}
