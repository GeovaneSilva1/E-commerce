using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.Controllers.Auth
{
    public class AuthController : Controller
    {

        private readonly IJwtService _jwtService;
        private readonly IAuthService _authService;

        public AuthController(IJwtService jwtService,
                              IAuthService authService)
        {
            _jwtService = jwtService;
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var token = await _authService.LoginAsync(model);

            await _jwtService.SalveJwt(Response, token, 1);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _authService.RegisterAsync(model);

            // Após o registro, redirecionar para a página de login ou outra página apropriada
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
