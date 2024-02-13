namespace ProductCatalog.Data.Products
{
    using ProductCatalog.Data.Models;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class RepositoryProductsCatalog : IRepositoryProductsCatalog
    {
        private readonly ApplicationDbContext _context;

        public RepositoryProductsCatalog(ApplicationDbContext context)
        {
            _context = context;
        }

        public Product GetId(int id)
        {
            return _context.Productos.Find(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Productos.ToList();
        }

        public Product Add(Product product)
        {
            _context.Productos.Add(product);
            _context.SaveChanges();
            _context.Entry(product).GetDatabaseValues();

            return product;
        }
        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.Productos.Find(id);
            if (product != null)
            {
                _context.Productos.Remove(product);
                _context.SaveChanges();
            }

        }

    }

}
