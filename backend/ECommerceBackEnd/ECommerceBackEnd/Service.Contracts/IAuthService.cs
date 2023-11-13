using ECommerceBackEnd.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackEnd.Service.Contracts
{ 
    public interface IAuthService 
    {
        //Task<IdentityResult> Register(CustomerAuthDto user);
        bool ValidateUser(CustomerAuthDto user);
        Task<string> CreateToken();
    }
}
