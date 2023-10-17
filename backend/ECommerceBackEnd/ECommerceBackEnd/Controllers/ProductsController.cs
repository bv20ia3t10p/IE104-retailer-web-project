using ECommerceBackEnd.Dtos.Product;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ECommerceBackEnd.Controllers
{

    [ApiController]
    [Route("items")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository repository;
        public ProductsController(IProductsRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        // GET /products
        public IEnumerable<ProductDto> GetProducts()
        {
            var products = repository.GetProducts().Select(product => product.AsDto()).ToList();
            return products;
        }
        // GET /products/id
        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            var product = repository.GetProduct(id);
            if ( product == null) {
                return NotFound();
            }
            return product.AsDto();
        }
        // POST /items
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct(CreateProductDto productDto)
        {
            Product product = new()
            {
                Id = ObjectId.GenerateNewId(),
                DepartmentId = productDto.DepId,
                DepartmentName = productDto.DName,
                ProductCardId = productDto.PID,
                ProductCategoryId = productDto.CID,
                ProductName = productDto.PName,
                ProductPrice = productDto.Price,
                OrderItemCardprodId = productDto.OPID,
                OrderItemId = productDto.OID,
                Sales = productDto.Gain,
                OrderItemProfitRatio = productDto.Ratio,
                ProductStatus = productDto.Available
            };
            repository.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new {id=product.ProductCardId},product.AsDto());
        }


    }
}
