using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface ICartService
    {
        Task AddProductToCart(int userId , ProductToCartDto productToCartDto);
        Task DecreaseCartItem(int ProductId, int CartId);
        Task DeleteItem(int productId, int CartId);

    }
}
