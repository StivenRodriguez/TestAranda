namespace ProductCatalog.Business.Interfaces
{
    using ProductCatalog.Business.DTOs;
    using System.Collections.Generic;

    public interface IProductService
    {
        ProductDTO AddProduct(ProductDTO product);
        IEnumerable<ProductDTO> GetProducts(string search = "", string sort = "", bool order = false,  int page = 1, int pageSize = 10);
        ProductDTO GetProductById(int productId);     
        void UpdateProduct(ProductDTO producto);
        void DeleteProduct(int id);

    }

}
