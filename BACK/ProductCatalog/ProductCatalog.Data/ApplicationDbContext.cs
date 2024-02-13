namespace ProductCatalog.Data
{
    using ProductCatalog.Data.Models;
    using System.Data.Entity;
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Productos { get; set; }

        public ApplicationDbContext() : base("ProductCatalogDBEntities")
        {

        }

        public DbSet<Product> Products { get; set; }
    }

}
