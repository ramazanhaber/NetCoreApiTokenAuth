using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreApiTokenAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        // NuGet\Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 7.0.14

        [Route("get")]
        [Authorize]
        [HttpGet]
        public string Get()
        {
            return "RAMBO";
        }

        [Route("loginol")]
        [HttpGet]
        public string login(string username)
        {
            return "Bearer " + GenerateToken(username);
        }

      
        private string GenerateToken(string username)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: new List<Claim>(),
                expires: DateTime.Now.AddHours(10),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;

        }

    }
}