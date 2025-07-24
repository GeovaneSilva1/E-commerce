using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
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

        public ProdutoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoVM)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            ProdutoViewModel produtoVMUpdate = new ProdutoViewModel();

            using (var response = await client.PutAsJsonAsync(_apiEndPoint, produtoVM))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoVMUpdate = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao atualizar produto: " + response.ReasonPhrase);
                }
            }
            return produtoVMUpdate;
        }

        public async Task<ProdutoViewModel> CriarProdutoAsync(ProdutoViewModel produtoVM)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            var content = new StringContent(JsonSerializer.Serialize(produtoVM), Encoding.UTF8, "application/json");
            
            using (var response = await client.PostAsync(_apiEndPoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _ProdutoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao criar produto: " + response.ReasonPhrase);
                }
            }
            return _ProdutoVM;
        }

        public async Task<bool> DeletarProdutoAsync(int id)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            return await client.DeleteAsync(_apiEndPoint + id)
                .ContinueWith(response =>
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Erro ao deletar produto: " + response.Result.ReasonPhrase);
                    }
                }); 
        }

        public async Task<ProdutoViewModel> ObterProdutoPorIdAsync(int id)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _ProdutoVM = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    throw new Exception("Erro ao obter produto: " + response.ReasonPhrase);
                }
            }
            return _ProdutoVM;

        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterProdutosAsync()
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            using (var response = await client.GetAsync(_apiEndPoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    _produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _jsonSerializerOptions);

                }
                else
                {
                    throw new Exception("Erro ao obter produtos: " + response.ReasonPhrase);
                }
            }
            return _produtosVM;
        }
    }
}
