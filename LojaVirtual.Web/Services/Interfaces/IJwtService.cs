namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IJwtService
    {
        Task RemoveJwt(HttpResponse response);
        Task SalveJwt(HttpResponse response, string token, long horasValidas = 1);
    }
}
