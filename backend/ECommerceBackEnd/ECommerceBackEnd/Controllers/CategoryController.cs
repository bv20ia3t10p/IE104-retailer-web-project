using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesRepository repository;
        public CategoryController(ICategoriesRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<CategoryDto> Get()
        {
            return repository.GetCategories().Select(category=>category.AsDto()).ToList();
        }


        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public ActionResult<CategoryDto> Get(int id)
        {
            var category = repository.GetCategory(id);
            if ( category == null)
            {
                return NotFound();
            }
            return category.AsDto();
        }

        // POST api/<CategoryController>
        [HttpPost]
        public ActionResult<CategoryDto> Post([FromBody] CreateCategoryDto value)
        {
            
            int insertId = (repository as CategoriesRepository).latestId;
            Category category = new()
            {
                Id = ObjectId.GenerateNewId(),
                CategoryName = value.CName,
                CategoryId = insertId
            };
            repository.CreateCategory(category);
            //repository.CreateCategory(value);
            return CreatedAtAction(nameof(Get), new { id = category.CategoryId},category.AsDto() );
            // return CreatedAtAction(nameof(Get), new { id = value.CategoryId }, value);
    }
        // TODO : Add Delete/Update for both products and categories
        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
