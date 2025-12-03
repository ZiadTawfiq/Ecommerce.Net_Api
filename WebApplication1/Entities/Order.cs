namespace WebApplication1.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? UserNumber { get; set; }
        public ApplicationUser? AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem>?OrderItems { get; set; }


    }
}
