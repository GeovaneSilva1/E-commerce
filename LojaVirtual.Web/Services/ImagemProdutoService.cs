using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.IO;
using System.IO.Pipes;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class ImagemProdutoService : IImagemProdutoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/ImagemProdutos";
        private const string _caminhoImagens = "\\images\\produtos\\";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly IJwtService _jwtService;
        private IEnumerable<ImagemProdutoViewModel> _imagensProdutoVMs;
        private ImagemProdutoViewModel _imagemProdutoVM;
        public static IWebHostEnvironment _webHostEnvironment;

        public ImagemProdutoService(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment, IJwtService jwtService)
        {
            _clientFactory = httpClientFactory;
            _webHostEnvironment = webHostEnvironment;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _jwtService = jwtService;
        }

        public async Task<IEnumerable<ImagemProdutoViewModel>> ObterImagensPorProdutoIdAsync(long produtoHandle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");

            using (var response = await client.GetAsync(_apiEndPoint + "/produto/" + produtoHandle))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _imagensProdutoVMs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagemProdutoViewModel>>(apiResponse, _jsonSerializerOptions);
                }
                else
                {
                    return Enumerable.Empty<ImagemProdutoViewModel>();
                }
            }
            return _imagensProdutoVMs;
        }

        public async Task<IEnumerable<ImagemProdutoViewModel>> UploadImagensAsync(IEnumerable<IFormFile> fileUpload, long produtoId)
        {
            foreach (var imagem in fileUpload)
            {
                if (imagem == null || imagem.Length == 0) continue;
                if (!Path.GetExtension(imagem.FileName).Equals(".webp", StringComparison.OrdinalIgnoreCase))
                    throw new Exception("Formato de imagem inválido. Apenas .webp.");

                var nomeArquivo = $"{Guid.NewGuid()}_{Path.GetFileName(imagem.FileName)}";
                var client = _clientFactory.CreateClient("CatalogoAPI");
                client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();

                using (var ms = new MemoryStream())
                {
                    ms.Position = 0;

                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new StreamContent(ms);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/webp");
                        content.Add(fileContent, "File", $"{_caminhoImagens}{nomeArquivo}");

                        var response = await client.PostAsync($"{_apiEndPoint}?produtoHandle={produtoId}", content);

                        var caminhoPasta = Path.Combine(_webHostEnvironment.WebRootPath, _caminhoImagens.Trim('\\', '/'));
                        if (!Directory.Exists(caminhoPasta))
                        {
                            Directory.CreateDirectory(caminhoPasta);
                        }
                        var caminhoCompleto = Path.Combine(caminhoPasta, nomeArquivo);

                        if (response.IsSuccessStatusCode)
                        {
                            ms.Position = 0;
                            await using (FileStream fs = File.Create(caminhoCompleto))
                            {
                                await imagem.CopyToAsync(fs);
                                fs.Flush();
                            }
                        }
                        else
                        {
                            var error = await response.Content.ReadAsStringAsync();
                            throw new Exception($"Erro ao enviar imagem: {error} ({response.StatusCode})");
                        }
                    }
                }
            }
            return await ObterImagensPorProdutoIdAsync(produtoId);
        }

        public async Task<IEnumerable<ImagemProdutoViewModel>> DeletarImagemAsync(long imagemHandle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            client.DefaultRequestHeaders.Authorization = await _jwtService.GetJwtAuthorizationHeader();
            try
            {
                using (var imagemDeletada = await client.GetAsync(_apiEndPoint + "/imagem/" + imagemHandle))
                {
                    if (!imagemDeletada.IsSuccessStatusCode)
                    {
                        var error = await imagemDeletada.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao buscar imagem: {error} ({imagemDeletada.StatusCode})");
                    }

                    var apiResponse = await imagemDeletada.Content.ReadAsStreamAsync();

                    _imagemProdutoVM = await JsonSerializer.DeserializeAsync<ImagemProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                }

                using (var response = await client.DeleteAsync(_apiEndPoint + "/" + imagemHandle))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao deletar imagem: {error} ({response.StatusCode})");
                    }

                    var caminhoarquivo = $"{_webHostEnvironment.WebRootPath}\\{_imagemProdutoVM.Url}";
                    if (File.Exists(caminhoarquivo))
                    {
                        File.Delete(caminhoarquivo);
                    }
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _imagensProdutoVMs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagemProdutoViewModel>>(apiResponse, _jsonSerializerOptions);

                    return _imagensProdutoVMs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
    }
}
