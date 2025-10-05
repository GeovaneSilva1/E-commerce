using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string _apiEndPoint = "api/auth/"; // URL base da API de autenticação
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public AuthService(IHttpClientFactory clientFactory, 
                           IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _httpContextAccessor = httpContextAccessor;
            
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<string> LoginAsync(LoginViewModel model)
        {
            var client = _clientFactory.CreateClient("IdentityAPI");
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(_apiEndPoint + "login", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception("Erro ao autenticar usuário: " + errorMessage);
                }

                var apiResponse = await response.Content.ReadFromJsonAsync<TokenResponseDTO>();
                
                return apiResponse.Token;
            }
        }

        public async Task<RegisterViewModel> RegisterAsync(RegisterViewModel model)
        {
            var client = _clientFactory.CreateClient("IdentityAPI");
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            using (var response =  await client.PostAsync(_apiEndPoint + "register", content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception("Erro ao registrar usuário: " + errorMessage);
                }

                var apiResponse = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<RegisterViewModel>(apiResponse, _jsonSerializerOptions);
            }
        }

        public async Task<UserDTO> UsuarioValido()
        {
            var client = _clientFactory.CreateClient("IdentityAPI");
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            if (token == null)
                return null;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.GetAsync(_apiEndPoint + "me"))
            {
                if (!response.IsSuccessStatusCode)
                {                     
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return null;
                }

                var user = await response.Content.ReadFromJsonAsync<UserDTO>();

                return user;
            }
        }
    }
}
