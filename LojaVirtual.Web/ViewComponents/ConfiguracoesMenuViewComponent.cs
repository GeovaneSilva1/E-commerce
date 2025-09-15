using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.ViewComponents
{
    public class ConfiguracoesMenuViewComponent : ViewComponent
    {
        public ConfiguracoesMenuViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
