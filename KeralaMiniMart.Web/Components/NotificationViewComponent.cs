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

    public class NotificationViewComponent : ViewComponent
    {
        private readonly INotificationService _notificationService;

        public NotificationViewComponent(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task<IViewComponentResult> InvokeAsync(NotificationFilter filter, bool IsPartial)
        {
            NotificationWrapperViewModel ResponseModel = await _notificationService.GetWrapperForIndexView(filter);
            ViewBag.ShowEmptyState = !IsPartial;
            return View(ResponseModel);
        }
    }
}
