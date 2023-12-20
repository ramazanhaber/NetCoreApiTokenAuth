using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
    public class CachController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        public CachController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }


        [Route("getCache")]
        [HttpGet]
        [Produces("text/plain")]
        [ResponseCache(Duration =60,Location =ResponseCacheLocation.Client)]
        public string getCache()
        {
            Thread.Sleep(5000);
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [Route("getCache3")]
        [HttpGet]
        [Produces("text/plain")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public string getCache3()
        {
            Thread.Sleep(5000);
            return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

        }

        [Route("getCache2")]
        [HttpGet]
        [Produces("text/plain")]
        public string getCache2()
        {
            bool cashvarmi = _cache.TryGetValue("keyim",out string deger);
            if (cashvarmi==false)
            {
                _cache.Set("keyim","RAMBO cache");
                return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

            }

            var cashvalue = _cache.Get<string>("keyim");

            return cashvalue;

        }

        [Route("getCache6")]
        [HttpGet]
        [Produces("text/plain")]
        public string GetCache6(int sayi)
        {
            // Her bir sayý için farklý önbellek anahtarý oluþtur
            string cacheKey = $"getCache6_{sayi}";

            // Önbellekte var mý kontrol et
            if (_cache.TryGetValue(cacheKey, out string cachedValue))
            {
                // Önbellekte bulunan deðeri kullanabilirsiniz
                return cachedValue;
            }
            else
            {
                // Önbellekte bulunmuyorsa, hesaplamayý yap
                string result = (sayi * 30).ToString();

                // Hesaplanan deðeri önbelleðe ekle
                _cache.Set(cacheKey, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60) // 60 saniye önbellekte sakla
                });

                return result;
            }
        }


        [Route("clearCache")]
        [HttpGet]
        public IActionResult ClearCache()
        {
            _cache.Remove("keyim");

            return Ok("Cache cleared");
        }


    }
}