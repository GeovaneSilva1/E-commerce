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

        public MarcaService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<MarcaViewModel>> ObterMarcasAsync()
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            IEnumerable<MarcaViewModel> marcasVM;

            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    marcasVM = await JsonSerializer.DeserializeAsync<IEnumerable<MarcaViewModel>>(apiResponse, _jsonSerializerOptions);

                }
                else
                {
                    return Enumerable.Empty<MarcaViewModel>();
                }
            }
            return marcasVM;
        }

        public async Task<MarcaViewModel> AdicionarMarcaAsync(MarcaViewModel marca)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.PostAsJsonAsync(_apiEndPoint, marca))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _marcaVM = await JsonSerializer.DeserializeAsync<MarcaViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao adicionar marca: " + response.ReasonPhrase);
                }
            }
            return _marcaVM;
        }
    }
}
