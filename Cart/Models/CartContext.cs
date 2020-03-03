using Microsoft.EntityFrameworkCore;

namespace Cart.Models
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {
        }

        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
    }
}