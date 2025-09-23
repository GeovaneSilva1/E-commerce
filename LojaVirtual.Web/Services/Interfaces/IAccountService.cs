using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models.IdentityModels;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AuthResponseDto> AutenticarUsuario(LoginViewModel model);
        Task<AuthResponseDto> RegistrarUsuario(RegisterViewModel model);
    }
}
