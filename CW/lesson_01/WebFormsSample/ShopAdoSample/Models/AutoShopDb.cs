namespace ShopAdoSample.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AutoShopDb : DbContext
    {
        public AutoShopDb()
            : base("name=AutoShopDbContext")
        {
        }

        public virtual DbSet<Models> Models { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Models>()
                .Property(e => e.Color)
                .IsUnicode(false);
        }
    }
}
