using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos.Product;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ECommerceBackEnd.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
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
            int insertId = (repository as ProductsRepository).latestId;
            Product product = new()
            {
                Id = ObjectId.GenerateNewId(),
                DepartmentId = productDto.DepId,
                DepartmentName = productDto.DName,
                ProductCardId = insertId,
                ProductCategoryId = productDto.CID,
                ProductName = productDto.PName,
                ProductPrice = productDto.Price,
                OrderItemCardprodId = insertId,
                OrderItemId = productDto.OID,
                Sales = productDto.Gain,
                OrderItemProfitRatio = productDto.Ratio,
                ProductStatus = productDto.Available
            };
            repository.CreateProduct(product);
            return CreatedAtAction(nameof(GetProduct), new {id=product.ProductCardId},product.AsDto());
        }

        [HttpGet]
        [Route(nameof(GetProductByCategory))]
        public IEnumerable<ProductDto> GetProductByCategory(int id)
        {
            return repository.GetProductByCategory(id).Select(product => product.AsDto()).ToList();
        }
    }
}
