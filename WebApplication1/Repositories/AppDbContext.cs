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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                   .HasIndex(u => u.UserNumber)
                   .IsUnique();

            builder.Entity<ApplicationUser>()
                .Property(_ => _.UserNumber)
                .ValueGeneratedOnAdd();

            builder.Entity<Cart>()
                .HasOne(c => c.AppUser)
                .WithOne(u => u.cart)
                .HasForeignKey<Cart>(c => c.UserNumber) 
                .HasPrincipalKey<ApplicationUser>(u => u.UserNumber);

            builder.Entity<ApplicationUser>()
                .HasMany(_ => _.Orders)
                .WithOne(_ => _.AppUser)
                .HasForeignKey(_ => _.UserNumber)
                .HasPrincipalKey(_ => _.UserNumber); 
                   

        }


        public DbSet<Product>Products { get; set;  }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }



        }
}
