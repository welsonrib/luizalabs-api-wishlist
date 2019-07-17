using Microsoft.EntityFrameworkCore;

namespace luizalabs_api_wishlist.Models.Entities
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Wish> Wishes { get; set; }
    }
}
