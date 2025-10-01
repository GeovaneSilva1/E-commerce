using LojaVirtual.Web.Services.Interfaces;

namespace LojaVirtual.Web.Services
{
    public class JwtService : IJwtService
    {
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
