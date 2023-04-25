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
   
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IWebCategoryService _webCategoryService;

        public CategoryViewComponent(IWebCategoryService webCategoryService)
        {
            _webCategoryService = webCategoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync(FilterBase filter)
        {
            CategoryWrapperViewModel ResponseModel = await _webCategoryService.GetWrapperForIndexView(filter);
            return View(ResponseModel);
        }
    }
}
