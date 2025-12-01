using WebApplication1.DTOs;

namespace WebApplication1.Services
{
    public interface ICartService
    {
        Task AddProductToCart(int userId , ProductToCartDto productToCartDto);
    }
}
