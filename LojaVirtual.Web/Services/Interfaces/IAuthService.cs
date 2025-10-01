
using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginViewModel model);
        Task<RegisterViewModel> RegisterAsync(RegisterViewModel model);
    }
}
