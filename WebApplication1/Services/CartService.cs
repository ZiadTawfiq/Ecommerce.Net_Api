using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CartService(IProductRepository productRepository , ICartRepository cartRepository) : ICartService
    {
        public async Task AddProductToCart(int UserId  , ProductToCartDto productToCartDto)
        {
            var product = await productRepository.GetByIdAsync(productToCartDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            var cart = await cartRepository.GetCartByUserId(UserId.ToString());
            if (cart == null)
            {
                cart = await cartRepository.CreateCart(UserId.ToString());
            }
            var cartItem = await  cartRepository.GetCartItemByCartIdAndProductId(cart.Id , productToCartDto.ProductId);

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }


        }
    }
}
