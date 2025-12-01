using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class CartRepository(AppDbContext Context) : ICartRepository
    {
        public async Task<Cart> CreateCart(string UserId)
        {
          var User  = await Context.Users.FirstOrDefaultAsync(u => u.Id == UserId.ToString());
            if (User == null)
            {
                throw new Exception("User not found");
            }
            var cart = new Cart
            {
                AppUserId = UserId,              
                TotalPrice = 0,
                CartItems = new List<CartItem>()
            };
            await Context.Carts.AddAsync(cart);
            await Context.SaveChangesAsync();
            return cart;

        }

        public async Task<Cart> GetCartByUserId(string UserId)
        {
            var Cart = await Context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.AppUserId == UserId);
         
            return Cart;

        }

        public async Task<CartItem> GetCartItemByCartIdAndProductId(int CartId, int ProductId)
        {
            var cartItem = await Context.CartItems.
                FirstOrDefaultAsync(_ => _.CartId == CartId && _.ProductId == ProductId);

            return cartItem;

        }
    }
}
