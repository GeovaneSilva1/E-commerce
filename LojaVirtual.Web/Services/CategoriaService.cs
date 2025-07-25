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
    }
}
