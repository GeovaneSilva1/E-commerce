using LojaVirtual.IdentityAPI.Context;
using LojaVirtual.IdentityAPI.DTOs;
using Microsoft.AspNetCore.Identity;

namespace LojaVirtual.IdentityAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> CreateUserAsync(RegisterUserDTO userDTO);
        Task<User> ExistsByEmailAsync(string email);
        Task<string> GenerateJwtTokenAsync(User user);
        Task<SignInResult> ValidaUserByPasswordAsync(User user, string password);
        Task RemoveUserTokenAsync();
        Task<User> ExistsByIdAsync(string userId);
        UserProfileDTO RetornaProfileUserDTO(User user);
    }
}
