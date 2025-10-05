using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/marcas/"; // URL base da API de marcas
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private MarcaViewModel _marcaVM;
        private readonly IJwtService _jwtService;

        public MarcaService(IHttpClientFactory clientFactory, IJwtService jwtService)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<MarcaViewModel>> ObterMarcasAsync()
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            IEnumerable<MarcaViewModel> marcasVM;

            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<MarcaViewModel>();
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                marcasVM = await JsonSerializer.DeserializeAsync<IEnumerable<MarcaViewModel>>(apiResponse, _jsonSerializerOptions);
            }
            return marcasVM;
        }

        public async Task<MarcaViewModel> AdicionarMarcaAsync(MarcaViewModel marca)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.PostAsJsonAsync(_apiEndPoint, marca))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao adicionar marca: {errorMessage} {response.StatusCode}");
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _marcaVM = await JsonSerializer.DeserializeAsync<MarcaViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _marcaVM;
        }

        public async Task<MarcaViewModel> ObterMarcaPorIdAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao obter marca: {errorMessage} {response.StatusCode}");
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _marcaVM = await JsonSerializer.DeserializeAsync<MarcaViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _marcaVM;
        }

        public async Task<MarcaViewModel> AtualizarMarcaAsync(MarcaViewModel marca)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.PutAsJsonAsync(_apiEndPoint, marca))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao atualizar marca: {errorMessage} {response.StatusCode}");
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _marcaVM = await JsonSerializer.DeserializeAsync<MarcaViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _marcaVM;
        }

        public async Task<bool> DeletarMarcaAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.DeleteAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao deletar marca: {errorMessage} {response.StatusCode}");
                }
                
                return await Task.FromResult(true);
            }
        }
    }
}
