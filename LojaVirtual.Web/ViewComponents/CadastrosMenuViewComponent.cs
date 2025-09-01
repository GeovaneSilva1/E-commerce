using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.ViewComponents
{
    public class CadastrosMenuViewComponent : ViewComponent
    {
        public CadastrosMenuViewComponent()
        {
        }
        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
