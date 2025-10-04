using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/produtos/"; // URL base da API de produtos
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private ProdutoViewModel _ProdutoVM;
        private IEnumerable<ProdutoViewModel> _produtosVM;
        private IHttpContextAccessor _httpContextAccessor;

        public ProdutoService(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoVM)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ProdutoViewModel produtoVMUpdate = new ProdutoViewModel();

            using (var response = await client.PutAsJsonAsync(_apiEndPoint, produtoVM))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    throw new Exception("Erro ao atualizar produto: " + errorMessage + statusCode);
                }

                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVMUpdate = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return produtoVMUpdate;
        }

        public async Task<ProdutoViewModel> CriarProdutoAsync(ProdutoViewModel produtoVM)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonSerializer.Serialize(produtoVM), Encoding.UTF8, "application/json");
            
            using (var response = await client.PostAsync(_apiEndPoint, content))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    throw new Exception($"Erro ao criar produto: {errorMessage} {statusCode}");
                }
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _ProdutoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
            }
            return _ProdutoVM;
        }

        public async Task<bool> DeletarProdutoAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            var token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await client.DeleteAsync(_apiEndPoint + handle))
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    throw new Exception("Erro na exclusão: " + errorMessage + statusCode);
                }
            }
            return true;
        }

        public async Task<ProdutoViewModel> ObterProdutoPorIdAsync(long handle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + handle))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _ProdutoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception("Erro ao obter produto: " + errorMessage);
                }
            }
            return _ProdutoVM;

        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosAsync()
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            

            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return Enumerable.Empty<ProdutoViewModel>();
                }

                var apiResponse = await response.Content.ReadAsStreamAsync();

                _produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _jsonSerializerOptions);
            }
            return _produtosVM;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosByCategoriaIdAsync(long? categoriaHandle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + $"categoria/{categoriaHandle}" ))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    return Enumerable.Empty<ProdutoViewModel>();
                }
            }
            return _produtosVM;
        }
    }
}
