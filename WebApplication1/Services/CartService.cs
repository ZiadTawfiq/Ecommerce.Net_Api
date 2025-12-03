using WebApplication1.DTOs;
using WebApplication1.Entities;
using WebApplication1.Repositories;

namespace WebApplication1.Services
{
    public class CartService(IProductRepository productRepository , ICartRepository cartRepository ) : ICartService
    {
        public async Task AddProductToCart(int UserId  , ProductToCartDto productToCartDto)
        {
            var product = await productRepository.GetByIdAsync(productToCartDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var cart = await cartRepository.GetCartByUserId(UserId);
            if (cart == null)
            {
                cart = await cartRepository.CreateCart(UserId);
            }
            var cartItem = await  cartRepository.GetCartItemByCartIdAndProductId(cart.Id , productToCartDto.ProductId);
            

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                var CartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    Quantity = 1

                };
                

                await cartRepository.AddCartItem(CartItem); 
                    
                
            }
            cart.TotalPrice = cart.CartItems?.Sum(_ => _.UnitPrice * _.Quantity)?? 0; 
          

            await cartRepository.SaveChanges(); 

        }
        public async Task DecreaseCartItem(int ProductId, int CartId)
        {
            var cart = await cartRepository.GetCartById(CartId);


            if (cart == null)
            {
                throw new Exception("There is no Cart!"); 
            }

            var cartItem = await cartRepository.GetCartItemByCartIdAndProductId(CartId, ProductId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");
            }
            cartItem.Quantity -= 1; 

            if (cartItem.Quantity <= 0)
            {
               await  cartRepository.DeletCartItem(CartId, ProductId); 
            }
            cart.TotalPrice = cart.CartItems?.Sum(_ => _.UnitPrice * _.Quantity) ?? 0;


            await cartRepository.SaveChanges(); 
            
        }
        public async Task DeleteItem(int productId, int CartId)
        {
            await cartRepository.DeletCartItem(productId, CartId);
            await cartRepository.SaveChanges(); 
        }

    }
}
