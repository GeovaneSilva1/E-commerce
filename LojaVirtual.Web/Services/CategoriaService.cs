using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/categorias/"; // URL base da API de categorias
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private CategoriaViewModel _categoriaVM;
        private IEnumerable<CategoriaViewModel> _categoriasVM;
        private readonly IJwtService _jwtService;

        public CategoriaService(IHttpClientFactory clientFactory, IJwtService jwtService)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync()
        {   
            var client = _clientFactory.CreateClient("CatalogoAPI");
            IEnumerable<CategoriaViewModel> categoriasVM;

            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<CategoriaViewModel>();
                    
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categoriasVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _jsonSerializerOptions);
            }
            return categoriasVM;

        }

        public async Task<CategoriaViewModel> AdicionarCategoriaAsync(CategoriaViewModel categoria)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.PostAsJsonAsync(_apiEndPoint, categoria))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao criar categoria: " + response.ReasonPhrase);
                }

                var apiResponse = await response.Content.ReadAsStreamAsync();
                _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _categoriaVM;
        }

        public async Task<CategoriaViewModel> ObterCategoriaPorIdAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao obter categoria: " + response.ReasonPhrase);
                }
                
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _categoriaVM;
        }

        public async Task<CategoriaViewModel> AtualizarCategoriaAsync(CategoriaViewModel categoria)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.PutAsJsonAsync(_apiEndPoint, categoria))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao atualizar categoria: {errorMessage} {response.StatusCode}");
                }
                
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
            }

            return _categoriaVM;
        }

        public async Task<bool> DeletarCategoriaAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            using (var response = await client.DeleteAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Erro ao deletar categoria: {errorMessage} {response.StatusCode}");
                }

                return await Task.FromResult(true);
            }
        }
    }
}
