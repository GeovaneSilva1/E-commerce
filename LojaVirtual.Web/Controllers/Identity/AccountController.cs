using LojaVirtual.Web.Models.IdentityModels;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtual.Web.Controllers.Identity
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Dados inválidos!");
                    return View();
                }

                // Lógica para registrar o usuário
                var usuarioToken = await _accountService.RegistrarUsuario(model);

                if (usuarioToken is null)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu falha no registro do usuário");
                    return View();
                }

                Response.Cookies.Append("jwt", usuarioToken.Token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuarioToken.PrimeiroNome),
                    new Claim(ClaimTypes.Email, usuarioToken.Email),
                    new Claim("jwt", usuarioToken.Token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Cria o cookie
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // mantém logado entre reinícios do browser
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // tempo de expiração
                    });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError(string.Empty, "Dados inválidos!");
                    return View();
                }
                // Lógica para autenticar o usuário
                var usuariotoken = await _accountService.AutenticarUsuario(model);

                if (usuariotoken is null)
                {
                    ModelState.AddModelError(string.Empty, "Usuário ou senha inválidos.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuariotoken.PrimeiroNome),
                    new Claim(ClaimTypes.Email, usuariotoken.Email),
                    new Claim("jwt", usuariotoken.Token)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // mantém logado entre reinícios do browser
                        ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // tempo de expiração
                    });

                Response.Cookies.Append("jwt", usuariotoken.Token, new CookieOptions { HttpOnly = true });

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
