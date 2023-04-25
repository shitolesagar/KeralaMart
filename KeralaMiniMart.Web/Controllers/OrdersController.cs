using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Entities.WebViewModels.DetailsPageViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KeralaMiniMart.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDeliveryStatusRepository _orderDeliveryStatusRepository;
        #region Private fields and Constructor
        public OrdersController(IOrderService orderService, IOrderDeliveryStatusRepository orderDeliveryStatusRepository)
        {
            _orderService = orderService;
            _orderDeliveryStatusRepository = orderDeliveryStatusRepository;
        }


        #endregion

        #region Index
        /// <summary>
        /// This method is for index view of orders
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(OrderFilter filter)
        {
            ViewBag.Filters = filter;
            List<IdNameViewModel> deliveryStatusList = await _orderService.GetAllDeliveryStatusAsync();
            ViewBag.DeliveryStatusList = deliveryStatusList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.WeekDays = WeekDays.AllWeekDays.Select(x => new SelectListItem() { Value = x.ToString(), Text = x });
            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of orders
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(OrderFilter filter)
        {
            return ViewComponent("Order", new { filter, IsPartial = true });
        }
        #endregion

        #region Details
        /// <summary>
        /// This method is for fetching order details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            List<IdNameViewModel> deliveryStatusList = await _orderService.GetAllDeliveryStatusAsync();
            ViewBag.DeliveryStatusList = deliveryStatusList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            OrderDetailsViewModel model = await _orderService.GetOrderDetails(id);
            return View(model);
        }
        #endregion

        #region Update Status
        /// <summary>
        /// This method is for Updating order status
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateStatus(UpdateStatusResource filter )
        {
            string  result =  await _orderService.UpdateStatus(filter);
            TempData["Message"] = MessageConstants.DeliveryStatusUpdated;
            //ViewBag.result = MessageConstants.DeliveryStatusUpdated;
            //return RedirectToAction("Index");
            return RedirectToAction("Details", new { @id = filter.Id });
        }
        #endregion
    }
}