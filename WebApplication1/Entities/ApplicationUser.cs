using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Entities
{
    public class ApplicationUser: IdentityUser
    {

      public int CartId { get; set;  }
      public Cart cart { get; set; }


    }
}
