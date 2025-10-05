using LojaVirtual.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace LojaVirtual.Web.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationHeaderValue> GetJwtAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            return new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task RemoveJwt(HttpResponse response)
        {
            response.Cookies.Delete("jwt");
        }

        public async Task SalveJwt(HttpResponse response, string token, long horasValidas = 1)
        {
            response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddHours(horasValidas)
            });
        }
    }
}
