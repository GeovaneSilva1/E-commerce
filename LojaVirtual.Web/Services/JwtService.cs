using LojaVirtual.Web.Services.Interfaces;

namespace LojaVirtual.Web.Services
{
    public class JwtService : IJwtService
    {
        public Task RemoveJwt(HttpResponse response)
        {
            throw new NotImplementedException();
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
