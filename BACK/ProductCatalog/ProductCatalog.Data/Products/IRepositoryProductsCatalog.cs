namespace ProductCatalog.Data.Products
{
    using ProductCatalog.Data.Models;
    using System.Collections.Generic;
    public interface IRepositoryProductsCatalog
    {
        Product Add(Product product);
        Product GetId(int id);
        IEnumerable<Product> GetAll();        
        void Update(Product producto);
        void Delete(int id);      
    }

}
