using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository repository;
        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        // GET /items
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            var products = repository.GetProducts();
            return products;
        }
        // GET /items/id
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(string id)
        {
            var product = repository.GetProduct(id);
            if (product is null)
            {
                return NotFound();
            }
            return product;
        }
        // POST /items
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product newProduct)
        {
            repository.CreateProduct(newProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = newProduct.ProductId });
        }
    }


}
