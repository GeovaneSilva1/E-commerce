using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models.IdentityModels;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/account/";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private RegisterViewModel _registerVM;

        public AccountService(IHttpClientFactory httpClientFactory) 
        {
            _clientFactory = httpClientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<AuthResponseDto> RegistrarUsuario(RegisterViewModel model)
        {
            var client = _clientFactory.CreateClient("Identity");
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            
            using (var response = await client.PostAsync(_apiEndPoint + "register" , content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
                
                var apiResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();

                return apiResponse;
            }
        }

        public async Task<AuthResponseDto> AutenticarUsuario(LoginViewModel model)
        {
            var client = _clientFactory.CreateClient("Identity");
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(_apiEndPoint + "login", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }

                var apiResponse = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                
                return apiResponse;
            }
        }
    }
}
