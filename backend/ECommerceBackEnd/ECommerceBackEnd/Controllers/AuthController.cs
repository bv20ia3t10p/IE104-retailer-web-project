using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Controllers
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
        public async Task<IActionResult> Authenticate([FromBody(EmptyBodyBehavior = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior.Allow)] CustomerAuthDto user,[FromHeader] string GoogleToken)
        {
            if (await _service.Auth.ValidateUser(user, GoogleToken)) return Ok(new { Token = await _service.Auth.CreateToken() });
            return Unauthorized();
        }
    }
}

