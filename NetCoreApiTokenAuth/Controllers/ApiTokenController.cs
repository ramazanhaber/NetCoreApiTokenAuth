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
    public class ApiTokenController : ControllerBase
    {
        // NuGet\Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 7.0.14

        [Route("getAdmin")]
        [Authorize(Roles = "admin")]
        [HttpGet]
        public string getAdmin()
        {
            return "RAMBO";
        }

        [Route("getMember")]
        [Authorize(Roles = "member")]
        [HttpGet]
        public string getMember()
        {
            return "RAMBO2";
        }

        [Route("loginol")]
        [HttpGet]
        public string login(string username, string role)
        {
            return "Bearer " + GenerateToken(username,role);
        }


        private string GenerateToken(string username, string role)
        {
            var claims = new List<Claim>{
                             new Claim(ClaimTypes.Name, username),
                             new Claim(ClaimTypes.Role, role)
                             };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-keyyour-secret-key"));

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(10),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;

        }

    }
}