using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NetCoreApiTokenAuth.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreApiTokenAuth.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiTokenController : ControllerBase
    {
        // NuGet\Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 7.0.14

        [Route("getAdmin")]
        [Authorize(Roles = "admin")] // sadece admin
        [HttpGet]
        [Produces("text/plain")]
        public string getAdmin()
        {
            return "RAMBO";
        }

        [Route("getMember")]
        [Authorize(Roles = "member")] // sadece member
        [HttpGet]
        [Produces("text/plain")]
        public string getMember()
        {
            return "RAMBO2";
        }

        [Route("getHerkes")]
        [Authorize] // herhangi bir token olsa yeterli
        [HttpGet]
        [Produces("text/plain")]
        public string getHerkes()
        {
            return "RAMBO3";
        }

        [Route("getHerkes2")]
        [HttpGet]
        [Produces("text/plain")]
        public string getHerkes2() // hiç tokene gerek yok
        {
            return "RAMBO32";
        }

        [Route("loginol")]
        [HttpGet]
        [Produces("text/plain")]
        public string login(string username, string role)
        {
            return "Bearer " + GenerateToken(username,role);
        }

        [Route("getOgrenci")]
        [HttpGet]
        public ActionResult<List<Ogrenci>> getOgrenci() // hiç tokene gerek yok
        {
            List<Ogrenci> list = new List<Ogrenci>();
            for (int i = 0; i < 10; i++)
            {
                Ogrenci ogrenci = new Ogrenci();
                ogrenci.ad = "rambo";
                ogrenci.id = i;
                list.Add(ogrenci);
            }
            
            return Ok(list);
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