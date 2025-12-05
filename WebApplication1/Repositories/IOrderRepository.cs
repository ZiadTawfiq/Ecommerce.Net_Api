using Microsoft.EntityFrameworkCore.Storage;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task DeleteOrder(Order order );
        Task<List<Order>> GetOrderByUserId(int userId);
        Task<Order> GetOrderById(int orderId);
        Task SaveChanges();
        Task<IDbContextTransaction> BeginTransaction();

    }
}
