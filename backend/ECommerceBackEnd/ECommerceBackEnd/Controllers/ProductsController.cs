using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IServiceManager _services;
        public ProductsController(IServiceManager service)
        {
            _services = service;
        }
        [HttpGet]
        // GET /products
        public ActionResult<IEnumerable<ProductDto>> GetProducts() => Ok(_services.Product.GetProducts());
        //GET /products/id
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id) => Ok(_services.Product.GetProductById(id));
        //POST /items
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            var product = _services.Product.CreateProduct(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductCardId }, product);
        }

        [HttpGet]
        [Route(nameof(GetProductByCategory))]
        public ActionResult<IEnumerable<ProductDto>> GetProductByCategory(int id) => Ok(_services.Product.GetProductByCategory(id));
        [HttpPut]
        public ActionResult<ProductDto> UpdateProduct(UpdateProductDto updateProduct) => Ok(_services.Product.UpdateProduct(updateProduct));
        [HttpDelete]
        public ActionResult DeleteProduct(int id)
        {
            _services.Product.DeleteProduct(id);
            return NoContent();
        }

    }
}
