using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.ViewComponents
{
    public class ImagensProdutoViewComponent : ViewComponent
    {
        private readonly IImagemProdutoService _imagemProdutoService;
        public static IWebHostEnvironment _webHostEnvironment;

        public ImagensProdutoViewComponent(IImagemProdutoService imagemProdutoService, IWebHostEnvironment webHostEnvironment)
        {
            _imagemProdutoService = imagemProdutoService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync(long produtoId)
        {
            var imagens = await _imagemProdutoService.ObterImagensPorProdutoIdAsync(produtoId);
            
            if (imagens != null && imagens.Any())
            {
                return View("Default", imagens);
            }
            
            return Content("");
        }
    }
}