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

        public CategoriaService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync()
        {   
            var client = _clientFactory.CreateClient("CatalogoAPI");
            IEnumerable<CategoriaViewModel> categoriasVM;

            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    categoriasVM = await JsonSerializer.DeserializeAsync<IEnumerable<CategoriaViewModel>>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    return Enumerable.Empty<CategoriaViewModel>();
                }
            }
            return categoriasVM;

        }

        public async Task<CategoriaViewModel> AdicionarCategoriaAsync(CategoriaViewModel categoria)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.PostAsJsonAsync(_apiEndPoint, categoria))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao criar categoria: " + response.ReasonPhrase);
                }
            }
            return _categoriaVM;
        }

        public async Task<CategoriaViewModel> ObterCategoriaPorIdAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + handle))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao obter categoria: " + response.ReasonPhrase);
                }
            }
            return _categoriaVM;
        }

        public async Task<CategoriaViewModel> AtualizarCategoriaAsync(CategoriaViewModel categoria)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.PutAsJsonAsync(_apiEndPoint, categoria))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _categoriaVM = await JsonSerializer.DeserializeAsync<CategoriaViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao atualizar categoria: " + response.Content.ReadAsStringAsync().Result);
                }
            }

            return _categoriaVM;
        }

        public async Task<bool> DeletarCategoriaAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.DeleteAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao deletar categoria: " + response.Content.ReadAsStringAsync().Result);
                }
                return await Task.FromResult(true);
            }
        }
    }
}
