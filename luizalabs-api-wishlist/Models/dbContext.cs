using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace luizalabs_api_wishlist.Models
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
