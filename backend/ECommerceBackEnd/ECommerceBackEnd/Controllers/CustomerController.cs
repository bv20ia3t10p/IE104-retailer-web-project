using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _services;
        public CustomerController(IServiceManager services)
        {
            _services = services;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDTO>> Get() => Ok(_services.Customer.GetCustomers());
        [HttpGet("{id}", Name = "GetCustomerById")]
        public ActionResult<CustomerDTO> Get(int id) => Ok(_services.Customer.GetCustomerById(id));
        [HttpPut]
        public ActionResult<CustomerDTO> Replace(CustomerDTO newCustomer) => Ok(_services.Customer.UpdateCustomer(newCustomer));
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _services.Customer.DeleteCustomerById(id);
            return NoContent();
        }
        [HttpPost]
        public ActionResult<CustomerDTO> Create(CustomerDTO newCustomer)
        {
            var newCustomerEntity = _services.Customer.CreateCustomer(newCustomer);
            return CreatedAtAction("GetCustomerById", new { id = newCustomerEntity.CustomerId }, newCustomerEntity);
        }
        [HttpPost("UpdateMultiplePW")]
        [Authorize(Roles ="ADMINISTRATOR")]
        public ActionResult<IEnumerable<CustomerDTO>> UpdateMultipleCustomerPasswords(string newPw)
        {
            _services.Customer.UpdateMultipleCustomerPassword(newPw);
            return Ok(_services.Customer.GetCustomers());
        }
    }
}
