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

    public class CustomerViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public CustomerViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IViewComponentResult> InvokeAsync(FilterBase filter, bool IsPartial)
        {
            CustomerWrapperViewModel ResponseModel = await _userService.GetWrapperForIndexView(filter);
            ViewBag.ShowEmptyState = !IsPartial;
            return View(ResponseModel);
        }
    }
}
