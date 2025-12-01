using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> CreateCart(string UserId);
        Task<Cart> GetCartByUserId(string UserId); 
        Task<CartItem> GetCartItemByCartIdAndProductId(int CartId , int ProductId);

    }
}
