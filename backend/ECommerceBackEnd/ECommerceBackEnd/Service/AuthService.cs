using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Service.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerceBackEnd.Service
{

    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repository;
        private readonly IConfiguration _configuration;
        private CustomerAuthDto? _user;

        public AuthService(IRepositoryManager repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public bool ValidateUser(CustomerAuthDto user)
        {
            var customerInDb = _repository.Customer.GetCustomerByEmail(user.CustomerEmail);
            _user = user;
            if (customerInDb == null || customerInDb.CustomerPassword != user.CustomerPassword)
            {
                return false;
            }
            return true;
        }
        private SigningCredentials GetSigningCredentials()
        {
            //var key = Encoding.UTF8.GetBytes(s: Environment.GetEnvironmentVariable("SECRET"));
            var key = Encoding.UTF8.GetBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
        private async Task<List<Claim>> GetClaims()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.CustomerEmail),
                new Claim(ClaimTypes.Role, "USER"),
                new Claim(JwtRegisteredClaimNames.Aud, jwtSettings["validAudience"]),
                new Claim(JwtRegisteredClaimNames.Iss,jwtSettings["validIssuer"])
            };
            return claims;
        }
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOtpions = new JwtSecurityToken
            (
               issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
            return tokenOtpions;
        }
    }
}
