using LojaVirtual.IdentityAPI.Context;
using LojaVirtual.IdentityAPI.DTOs;
using LojaVirtual.IdentityAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LojaVirtual.IdentityAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly User _user;
        private readonly UserProfileDTO _userProfileDTO;

        public AuthService(UserManager<User> userManager, 
                           SignInManager<User> signInManager, 
                           IConfiguration configuration,
                           User user,
                           UserProfileDTO userProfileDTO)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _user = user;
            _userProfileDTO = userProfileDTO;
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUserDTO userDTO)
        {
            _user.UserName = userDTO.Email;
            _user.Email = userDTO.Email;
            _user.PrimeiroNome = userDTO.PrimeiroNome;
            _user.UltimoNome = userDTO.UltimoNome;

            return await _userManager.CreateAsync(_user, userDTO.Password!);
        }

        public async Task<User> ExistsByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> ValidaUserByPasswordAsync(User user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserProfileDTO RetornaProfileUserDTO(User user)
        {
            _userProfileDTO.Id = user.Id;
            _userProfileDTO.Email = user.Email;
            _userProfileDTO.PrimeiroNome = user.PrimeiroNome;
            _userProfileDTO.UltimoNome = user.UltimoNome;

            return _userProfileDTO;

        }

        public async Task RemoveUserTokenAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> ExistsByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
