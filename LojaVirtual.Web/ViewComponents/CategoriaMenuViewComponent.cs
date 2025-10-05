using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.ViewComponents
{
    public class CategoriaMenuViewComponent : ViewComponent
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaMenuViewComponent(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categorias = await _categoriaService.ObterCategoriasAsync();
            return View(categorias);
        }
    }
}