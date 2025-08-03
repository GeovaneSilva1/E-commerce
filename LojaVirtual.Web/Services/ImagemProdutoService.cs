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

        public async Task<IEnumerable<ImagemProdutoViewModel>> ObterImagensPorProdutoIdAsync(long produtoId)
        {
               var client = _clientFactory.CreateClient("CatalogoAPI");

               using (var response = await client.GetAsync(_apiEndPoint + produtoId))
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

                    var nomeArquivo = Guid.NewGuid() + Path.GetExtension(imagem.FileName);
                    var caminhoarquivo = _webHostEnvironment.WebRootPath + "\\images\\produtos\\";
                    if (!Directory.Exists(caminhoarquivo))
                    {
                        Directory.CreateDirectory(caminhoarquivo);
                    }
                    using (FileStream fileStream = File.Create(caminhoarquivo + nomeArquivo))
                    {
                        await imagem.CopyToAsync(fileStream);
                        fileStream.Flush();
                    }
                    using (var fileStream = new FileStream(caminhoarquivo + nomeArquivo, FileMode.Open, FileAccess.Read))
                    using (var content = new MultipartFormDataContent())
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/webp");
                        content.Add(fileContent, "File", caminhoarquivo+nomeArquivo);

                        var client = _clientFactory.CreateClient("CatalogoAPI");

                        var response = await client.PostAsync($"{_apiEndPoint}?produtoHandle={produtoId}", content);
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Erro ao enviar imagem: " + response.ReasonPhrase);
                        }
                    }
                }
            }
            return await ObterImagensPorProdutoIdAsync(produtoId);
        }
    }
}
