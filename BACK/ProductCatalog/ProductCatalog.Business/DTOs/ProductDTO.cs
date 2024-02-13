namespace ProductCatalog.Business.DTOs
{
    public class ProductDTO
    {
        public int productId { get; set; }
        public string nameProduct { get; set; }
        public string shortDescriptionProduct { get; set; }
        public string categoryProduct { get; set; }
        public byte[] productImage { get; set; }
    }

}
