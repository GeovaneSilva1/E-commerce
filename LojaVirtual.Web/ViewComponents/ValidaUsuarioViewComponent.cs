using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.ViewComponents
{
    public class ValidaUsuarioViewComponent : ViewComponent
    {
        private readonly IAuthService _authService;
        public ValidaUsuarioViewComponent(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UserDTO usuarioValido = await _authService.UsuarioValido();

            ViewBag.Id = (usuarioValido != null) ? usuarioValido.Id : null;
            ViewBag.PrimeiroNome = (usuarioValido != null) ? usuarioValido.PrimeiroNome : null;
            ViewBag.UltimoNome = (usuarioValido != null) ? usuarioValido.UltimoNome : null;
            ViewBag.Email = (usuarioValido != null) ? usuarioValido.Email : null;

            return View("_LoginPartial");
        }
    }
}
