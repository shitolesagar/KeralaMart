using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MCommerceMart.Core.Web.Components
{
    public class SideMenuBarViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string activetab)
        {
            ViewData["ActiveTab"] = activetab;
            return View();
        }
    }
}