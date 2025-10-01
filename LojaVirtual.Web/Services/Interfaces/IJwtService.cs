namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IJwtService
    {
        Task SalveJwt(string token);
    }
}
