using System.Data.Entity;
using web_app_asp_net_mvc_code_first.Models.Entities;

namespace web_app_asp_net_mvc_code_first.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<StoreBranch> StoreBranches { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<City> Cities { get; set; }

        public StoreContext() : base("StoreEntity")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasOptional(x => x.ProductImage).WithRequired().WillCascadeOnDelete(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}