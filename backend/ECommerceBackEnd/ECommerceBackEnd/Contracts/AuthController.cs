using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Contracts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IServiceManager _service;
        public AuthController(IServiceManager services)
        {
            _service = services;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate(CustomerAuthDto user)
        {
            if (_service.Auth.ValidateUser(user)) return Ok(new { Token = await _service.Auth.CreateToken() });
            return Unauthorized();

        }
    }
}
