using coremvctest.Models;
using Microsoft.EntityFrameworkCore;

namespace coremvctest.Contexts
{
    public class ShopContext : DbContext
    {
        public DbSet<Good> Good {get;set;}
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
    }
}