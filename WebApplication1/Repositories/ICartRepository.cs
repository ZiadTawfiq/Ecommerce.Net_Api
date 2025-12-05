using MimeKit.Tnef;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateCart(int UserId);
        Task<Cart> GetCartById(int CartId);
        Task<Cart> GetCartByUserId(int  UserId); 
        Task<CartItem> GetCartItemByCartIdAndProductId(int CartId , int ProductId);
        Task<List<CartItem>> GetCartItemsByCartId(int CartId);
        Task AddCartItem(CartItem cartItem);
        Task DeletCartItem(int productId , int CartId);
        Task ClearCart(int cartId);
        Task SaveChanges();
     


    }
}
