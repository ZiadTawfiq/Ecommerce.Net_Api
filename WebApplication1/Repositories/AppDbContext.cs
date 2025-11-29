using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Repositories
{
    public class AppDbContext:IdentityDbContext<ApplicationUser> // يعني ال dbcontext او identitydbcontext زي ال box اللي فيه ال tables للي موجوده في ال identity tables االلي انا عايز اضيفها وال  
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        public DbSet<Product>Products { get; set;  }
    }
}
