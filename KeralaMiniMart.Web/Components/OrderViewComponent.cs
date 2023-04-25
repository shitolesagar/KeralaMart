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

    public class OrderViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;

        public OrderViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IViewComponentResult> InvokeAsync(OrderFilter filter, bool IsPartial)
        {

            OrderWrapperViewModel ResponseModel = await _orderService.GetWrapperForIndexView(filter);
            ViewBag.ShowEmptyState = !IsPartial;
            return View(ResponseModel);
        }
    }
}
