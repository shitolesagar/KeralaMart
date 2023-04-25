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

    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(ProductFilter filter, bool IsPartial)
        {
            ProductWrapperViewModel ResponseModel = await _productService.GetWrapperForIndexView(filter);
            ViewBag.ShowEmptyState = !IsPartial;
            return View(ResponseModel);
        }
    }
}
