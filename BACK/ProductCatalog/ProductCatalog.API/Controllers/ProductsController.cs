namespace ProductCatalog.API.Controllers
{
    using ProductCatalog.Business.DTOs;
    using ProductCatalog.Business.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class ProductsController : ApiController
    {
        private readonly IProductService _productoService;

        public ProductsController(IProductService productoService)
        {
            _productoService = productoService ?? throw new ArgumentNullException(nameof(productoService));
        }

        // POST api/products
        public IHttpActionResult Post([FromBody] ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("El producto no puede ser nulo");
                }

                ProductDTO addedProduct = _productoService.AddProduct(product);

                return CreatedAtRoute("DefaultApi", new { id = addedProduct.productId }, addedProduct);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {
                return InternalServerError();
            }

        }

        // GET api/products
        public IHttpActionResult Get(string search = "", string sort = "", bool order = false, int page = 1, int pageSize = 10)
        {
            try
            {
                IEnumerable<ProductDTO> products = _productoService.GetProducts(search, sort, order, page, pageSize);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // GET api/products/5
        public IHttpActionResult GetProductId(int id)
        {
            try
            {
                ProductDTO product = _productoService.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }

            catch (Exception)
            {
                return InternalServerError();
            }

        }

        // DELETE api/products/5
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _productoService.DeleteProduct(id);
                return Ok();
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {
                return InternalServerError();
            }

        }

        // PUT api/products/5
        public IHttpActionResult Put(int id, [FromBody] ProductDTO product)
        {
            try
            {
                if (product == null || id != product.productId)
                {
                    return BadRequest("Datos no válidos");
                }

                _productoService.UpdateProduct(product);

                return Ok(product);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {
                return InternalServerError();
            }

        }

    }

}
