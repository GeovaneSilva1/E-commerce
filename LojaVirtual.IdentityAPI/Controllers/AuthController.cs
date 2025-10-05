using LojaVirtual.IdentityAPI.Context;
using LojaVirtual.IdentityAPI.DTOs;
using LojaVirtual.IdentityAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LojaVirtual.IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService authService)
        {
            _AuthService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDTO userDTO)
        {
            if (userDTO is null)
            {
                return BadRequest("Dados do usuário inválidos.");
            }

            var user = await _AuthService.CreateUserAsync(userDTO);

            if (!user.Succeeded)
            {
                var errors = user.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            if (userDTO is null)
                return BadRequest("Dados do usuário inválidos.");

            User user = await _AuthService.ExistsByEmailAsync(userDTO.Email!);

            if (user is null)
                return Unauthorized("Usuário não cadastrado");

            var result = await _AuthService.ValidaUserByPasswordAsync(user, userDTO.Password!);
            if (!result.Succeeded)
                return Unauthorized("Senha inválida");

            string token = await _AuthService.GenerateJwtTokenAsync(user);

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            User user = await _AuthService.ExistsByIdAsync(userId);

            if (user is null)
                return NotFound("Usuário não encontrado.");

            UserProfileDTO userProfile = _AuthService.RetornaProfileUserDTO(user);

            return Ok(userProfile);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _AuthService.RemoveUserTokenAsync();
            return Ok();
        }
    }
}
