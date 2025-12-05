using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTOs;
using WebApplication1.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Repositories
{
    public class CartRepository(AppDbContext Context) : ICartRepository
    {
        public async Task AddCartItem(CartItem cartItem)
        {
            await Context.CartItems.AddAsync(cartItem);

        }

        public async Task ClearCart(int cartId)
        {
            var cart = await GetCartById(cartId);

            if (cart == null)
                throw new Exception("Cart not found");

            Context.CartItems.RemoveRange(cart.CartItems.ToList());

            cart.TotalPrice = 0;

            await SaveChanges();
        }

        public async Task<Cart> CreateCart(int UserId)
        {
          var User  = await Context.Users.FirstOrDefaultAsync(_ =>_.UserNumber == UserId);
            if (User == null)
            {
                throw new Exception("User not found");
            }
            var cart = new Cart
            {
                UserNumber = UserId,
      
                TotalPrice = 0,
                CartItems = new List<CartItem>()
            };
            await Context.Carts.AddAsync(cart);
            await SaveChanges();
            return cart;

        }

        public async Task DeletCartItem(int ProductId, int CartId)
        {
            var cartItem =await  GetCartItemByCartIdAndProductId(CartId, ProductId);
            var cart = await Context.Carts.FirstOrDefaultAsync(_ => _.Id == CartId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found");

            }
            
            Context.CartItems.Remove(cartItem);
            await SaveChanges();

            cart.TotalPrice = await Context.CartItems
               .Where(i => i.CartId == cart.Id)
               .SumAsync(i => i.Quantity * i.UnitPrice);
        }

        public  async Task<Cart> GetCartById(int CartId)
        {
            return await Context.Carts.Include(_ =>_.CartItems)
                .FirstOrDefaultAsync(_ => _.Id == CartId); 
        }

        public async Task<Cart> GetCartByUserId(int UserId)
        {
            var Cart = await Context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserNumber == UserId);
         
            return Cart;

        }

        public async Task<CartItem> GetCartItemByCartIdAndProductId(int CartId, int ProductId)
        {
            var cartItem = await Context.CartItems.
                FirstOrDefaultAsync(_ => _.CartId == CartId && _.ProductId == ProductId);

            return cartItem;

        }

        public Task<List<CartItem>> GetCartItemsByCartId(int CartId)
        {
            var CartItems = Context.CartItems
                .Where(_ => _.CartId == CartId)
                .ToListAsync();
            return CartItems;
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync(); 
        }
    }
}
