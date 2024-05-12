using Bussiness.Product;
using Bussiness.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bussiness.db
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        public DbSet<Products> Products { get; set; }    
        public DbSet<Users> Users { get; set; }    
        public DbSet<Orders.Orders> Orders { get; set; }
        public DbSet<OrderItems.OrderItems> OrderItems { get; set; }
        public DbSet<Cart.Cart> Cart { get; set; }
    }
}
