namespace WebApplication1.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public ApplicationUser AppUser { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
