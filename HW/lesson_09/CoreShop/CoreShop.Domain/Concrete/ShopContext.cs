using CoreShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoreShop.Domain.Concrete
{
    public class ShopContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Good> Good { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }

    }
}