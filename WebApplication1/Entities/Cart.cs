namespace WebApplication1.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public double TotalPrice { get; set;  }
    }
}
