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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await _authService.LoginAsync(model);

                await _jwtService.SalveJwt(Response, token);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return View(model);
            }
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

            try
            {
                await _authService.RegisterAsync(model);

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return View(model);
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _jwtService.RemoveJwt(Response);
            return RedirectToAction("Index", "Home");
        }
    }
}
