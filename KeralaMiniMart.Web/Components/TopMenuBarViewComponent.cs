using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MCommerceMart.Core.Web.Components
{
    public class TopMenuBarViewComponent : ViewComponent
    {
        // NOTE : If you don't have any data to pre-procss then you can convert it into partial-view.

        public async Task<IViewComponentResult> InvokeAsync(string tabHeading, string tabButton, string tabSearch)
        {
            ViewData["TabHeading"] = tabHeading;
            ViewData["TabButton"] = tabButton;
            ViewData["TabSearch"] = tabSearch;
            return View();
        }
    }
}