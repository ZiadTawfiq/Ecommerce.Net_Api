using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class OrderRepository(AppDbContext context) : IOrderRepository
    {
        public async Task AddOrder(Order order)
        {
            await context.Orders.AddAsync(order);
        }

     
        public async Task DeleteOrder(Order order)
        {
            var Order =  await context.Orders
                .Include(_ => _.OrderItems)
                .FirstOrDefaultAsync(_ => _.Id == order.Id);
            context.OrderItems.RemoveRange(Order.OrderItems);
            context.Orders.Remove(order);
        }
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await context.Database.BeginTransactionAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var Order = await context.Orders
                .Include(_ => _.OrderItems)
                .FirstOrDefaultAsync(_ => _.Id == orderId);
            return Order;
                
        }

        public async Task<List<Order>> GetOrderByUserId(int userId)
        {
            var Orders = await context.Orders
                .Include(_ => _.OrderItems)
                .Where(_ => _.UserNumber == userId)
                .ToListAsync();
            return Orders;
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync(); 
        }
    }
}
