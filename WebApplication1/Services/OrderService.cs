using System.Transactions;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Services;

namespace WebApplication1.Repositories
{
    public class OrderService(IOrderRepository orderRepository , ICartRepository cartRepository,IProductRepository productRepository, IEmailService EmailService,IUserRepository userRepository ) : IOrderService
    {
        public async Task CancelOrder(int OrderId)
        {
            var Order = await orderRepository.GetOrderById(OrderId); 
            if (Order == null)
            {
                throw new Exception("Order Not Found");
            
            }
            foreach (var orderItem in Order.OrderItems)
            {
                var product = await productRepository.GetByIdAsync(orderItem.ProductId);
                product.StockSize += orderItem.Quantity; 
            }

            await orderRepository.DeleteOrder(Order);

            await orderRepository.SaveChanges();

        }

        public async Task CreateOrder(OrderDto orderDto)
        {
            using var transaction = await orderRepository.BeginTransaction();
            try
            {
                var CartItems = await cartRepository.GetCartItemsByCartId(orderDto.CartId);

                if (CartItems.Count == 0)
                {
                    throw new Exception("No Items found in cart");
                }

                var OrderItems = new List<OrderItem>();
                foreach (var item in CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = item.ProductId,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,

                    };
                    var product = await productRepository.GetByIdAsync(item.ProductId);
                    if (product.StockSize - item.Quantity < 0)
                    {
                        throw new Exception($"The {product.Name} insufficient");
                    }
                    product.StockSize -= item.Quantity;
                    OrderItems.Add(orderItem);

                }

                var order = new Order
                {
                    OrderItems = OrderItems,
                    CreatedAt = DateTime.UtcNow,
                    UserNumber = orderDto.UserId,
                    TotalPrice = OrderItems.Sum(_ => _.TotalPrice),

                };

                var user = await userRepository.GetUserById(orderDto.UserId);
                if (user == null)
                {
                    throw new Exception("User not found!");
                }

                await orderRepository.AddOrder(order);
                
                await cartRepository.ClearCart(orderDto.CartId);
                
                await orderRepository.SaveChanges();
                
                await transaction.CommitAsync(); 

                await EmailService.SendEmailAsync(user.Email, "Order Confirmation",
                        $"Your order #{order.Id} has been created successfully!");

            }
            catch
            {
                transaction.Rollback();
                throw; 
            }
               
        }
    }
}
