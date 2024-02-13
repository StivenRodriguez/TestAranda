namespace ProductCatalog.Business.Implementations
{
    using AutoMapper;
    using ProductCatalog.Business.DTOs;
    using ProductCatalog.Business.Interfaces;
    using ProductCatalog.Data.Models;
    using ProductCatalog.Data.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private readonly IRepositoryProductsCatalog _productRepository;

        public ProductService(IRepositoryProductsCatalog productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            InitializeMapper();
        }

        private void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
            }
            
            );
        }

        public IEnumerable<ProductDTO> GetProducts(string search = "", string sort = "", bool order = false, int page = 1, int pageSize = 10)
        {
          
            try
            {
                ValidateSortParameter(sort);
                var query = _productRepository.GetAll();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();

                    query = query.Where(p =>
                        p.nameProduct.ToLower().Contains(search) ||
                        p.shortDescriptionProduct.ToLower().Contains(search) ||
                        p.categoryProduct.ToLower().Contains(search)
                    );
                }

                switch (sort)
                {
                    case "nombre":
                        query = order ? query.OrderBy(p => p.nameProduct) : query.OrderByDescending(p => p.nameProduct);
                        break;
                    case "descripcion":
                        query = order
                            ? query.OrderBy(p => p.nameProduct).ThenBy(p => p.shortDescriptionProduct)
                            : query.OrderByDescending(p => p.nameProduct).ThenByDescending(p => p.shortDescriptionProduct);
                        break;
                    case "categoria":
                        query = order
                            ? query.OrderBy(p => p.nameProduct).ThenBy(p => p.categoryProduct)
                            : query.OrderByDescending(p => p.nameProduct).ThenByDescending(p => p.categoryProduct);
                        break;
                }

                var result = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return result.Select(p => Mapper.Map<ProductDTO>(p));

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ProductDTO GetProductById(int productId)
        {
            Product product = _productRepository.GetId(productId);

            if (product == null)
            {
                throw new ArgumentException($"No se encontró un producto con ID {productId}");
            }

            return Mapper.Map<ProductDTO>(product);
        }

        public ProductDTO AddProduct(ProductDTO product)
        {
            ValidateProductNotNull(product);

            Product mappedProduct = Mapper.Map<Product>(product);
            _productRepository.Add(mappedProduct);

            return Mapper.Map<ProductDTO>(mappedProduct);
        }

        public void UpdateProduct(ProductDTO productDto)
        {
            ValidateProductNotNull(productDto);

            Product existingProduct = _productRepository.GetId(productDto.productId);

            if (existingProduct == null)
            {
                throw new ArgumentException($"No se encontró un producto con ID {productDto.productId}");
            }

            Mapper.Map(productDto, existingProduct);
            _productRepository.Update(existingProduct);
        }

        public void DeleteProduct(int id)
        {
            Product productToDelete = _productRepository.GetId(id);

            if (productToDelete == null)
            {
                throw new ArgumentException($"No se encontró un producto con ID {id}");
            }

            _productRepository.Delete(id);
        }

        private static void ValidateProductNotNull(ProductDTO product)
        {
            if (product == null)
            {
                throw new ArgumentException("El producto no puede ser nulo", nameof(product));
            }
        }

        private static void ValidateSortParameter(string sort)
        {
            if (string.IsNullOrWhiteSpace(sort) || (sort != "nombre" && sort != "descripcion" && sort != "categoria"))
            {
                throw new ArgumentException("El parámetro 'sort' solo admite 'nombre', 'descripcion' o 'categoria'");
            }

        }

    }

}
