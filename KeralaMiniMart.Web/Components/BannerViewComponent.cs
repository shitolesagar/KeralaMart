using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Web.Components
{

    public class BannerViewComponent : ViewComponent
    {
        private readonly IBannerService _bannerService;

        public BannerViewComponent(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        public async Task<IViewComponentResult> InvokeAsync(BannerFilter filter, bool IsPartial)
        {
            BannerWrapperViewModel ResponseModel = await _bannerService.GetWrapperForIndexView(filter);
            ViewBag.ShowEmptyState = !IsPartial;
            return View(ResponseModel);
        }
    }
}
