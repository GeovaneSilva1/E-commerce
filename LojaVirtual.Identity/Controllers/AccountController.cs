using LojaVirtual.Identity.Context;
using LojaVirtual.Identity.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaVirtual.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<ApplicationUser> userManager, 
                                 SignInManager<ApplicationUser> signInManager, 
                                 IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Refatorar esse método separando o create e o signIn para a camada model.
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                var user = new ApplicationUser
                {
                    PrimeiroNome = registerDTO.PrimeiroNome,
                    UltimoNome = registerDTO.UltimoNome,
                    UserName = registerDTO.Email,
                    Email = registerDTO.Email
                };

                var usuario = await _userManager.FindByEmailAsync(registerDTO.Email);
                
                if (usuario is not null)
                {
                    return BadRequest("Usuário já cadastrado!");
                }

                //Armazenar o usuário no banco de dados (tabela AspNetUsers)
                var result = await _userManager.CreateAsync(user, registerDTO.Senha);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                //await _signInManager.SignInAsync(user, isPersistent: false);
                var tokenjwt = GenerateToken(user);

                registerDTO.Token = tokenjwt;
                registerDTO.UserName = user.UserName;

                return Ok(registerDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Refatorar esse método para a camada model.
        /// <paramref name="loginDTO"/>
        /// returns>Retorna login válido ou inválido</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Senha, loginDTO.LembrarMe, lockoutOnFailure: false);
            
            if (!result.Succeeded)
            {
                return Unauthorized(result);
            }

            var usuario = await _userManager.FindByEmailAsync(loginDTO.Email);
            var token = GenerateToken(usuario);

            loginDTO.UserName = usuario.UserName;
            loginDTO.Token = token;
            loginDTO.PrimeiroNome = usuario.PrimeiroNome;

            return Ok(loginDTO);
        }

        /// <summary>
        /// Refatorar esse método para a camada model.
        private string GenerateToken(ApplicationUser usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            byte[] key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            /*var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            ); 

            return new JwtSecurityTokenHandler().WriteToken(token); */
        }
    }
}
