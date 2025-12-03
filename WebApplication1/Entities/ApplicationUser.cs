using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public int UserNumber { get; set;  }
        public Cart? cart { get; set; }
        public ICollection<Order> ?Orders { get; set;  }



    }
}
