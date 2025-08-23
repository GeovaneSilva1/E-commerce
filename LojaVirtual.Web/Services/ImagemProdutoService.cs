using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LojaVirtual.Web.Services
{
    public class ImagemProdutoService : IImagemProdutoService
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string _apiEndPoint = "api/ImagemProdutos"; // URL base da API de imagens de produtos
        private const string _caminhoImagens = "\\images\\produtos\\";
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private IEnumerable<ImagemProdutoViewModel> _imagensProdutoVMs;
        private ImagemProdutoViewModel _imagemProdutoVM;
        public static IWebHostEnvironment _webHostEnvironment;

        public ImagemProdutoService(IHttpClientFactory httpClientFactory, IWebHostEnvironment webHostEnvironment)
        {
            _clientFactory = httpClientFactory;
            _webHostEnvironment = webHostEnvironment;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
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
                if (imagem is not null && imagem.Length > 0)
                {
                    if (!Path.GetExtension(imagem.FileName).Equals(".webp"))
                    {
                        throw new Exception("Formato de imagem inválido. Apenas arquivos .webp são permitidos.");
                    }

                    var nomeArquivo = Guid.NewGuid() + imagem.FileName;
                    var caminhoarquivo = $"{_webHostEnvironment.WebRootPath}{_caminhoImagens}";
                    if (!Directory.Exists(caminhoarquivo))
                    {
                        Directory.CreateDirectory(caminhoarquivo);
                    }
                    using (FileStream fileStream = File.Create($"{caminhoarquivo}{nomeArquivo}"))
                    {
                        await imagem.CopyToAsync(fileStream);
                        fileStream.Flush();
                    }
                    using (var fileStream = new FileStream(caminhoarquivo + nomeArquivo, FileMode.Open, FileAccess.Read))
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/webp");
                        content.Add(fileContent, "File", $"{_caminhoImagens}{nomeArquivo}");

                        var client = _clientFactory.CreateClient("CatalogoAPI");

                        var response = await client.PostAsync($"{_apiEndPoint}?produtoHandle={produtoId}", content);
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Erro ao enviar imagem: " + response.Content.ReadAsStringAsync());
                        }
                    }
                }
            }
            return await ObterImagensPorProdutoIdAsync(produtoId);
        }

        public async Task<IEnumerable<ImagemProdutoViewModel>> DeletarImagemAsync(long imagemHandle)
        {
            var client = _clientFactory.CreateClient("CatalogoAPI");
            try
            {
                using (var imagemDeletada = await client.GetAsync(_apiEndPoint + "/imagem/" + imagemHandle))
                {
                    if (!imagemDeletada.IsSuccessStatusCode)
                    {
                        throw new Exception(imagemDeletada.Content.ReadAsStringAsync().Result);
                    }
                    
                    var apiResponse = await imagemDeletada.Content.ReadAsStreamAsync();

                    _imagemProdutoVM = await JsonSerializer.DeserializeAsync<ImagemProdutoViewModel>(apiResponse, _jsonSerializerOptions);
                    var caminhoarquivo = $"{_webHostEnvironment.WebRootPath}\\{_imagemProdutoVM.Url}";
                    if (File.Exists(caminhoarquivo))
                    {
                        File.Delete(caminhoarquivo);
                    }
                }

                using (var response = await client.DeleteAsync(_apiEndPoint + "/" + imagemHandle))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.Content.ReadAsStringAsync().Result);
                    }

                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    _imagensProdutoVMs = await JsonSerializer.DeserializeAsync<IEnumerable<ImagemProdutoViewModel>>(apiResponse, _jsonSerializerOptions);

                    return _imagensProdutoVMs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar imagem: " + ex.Message);

            }
        }
    }
}
