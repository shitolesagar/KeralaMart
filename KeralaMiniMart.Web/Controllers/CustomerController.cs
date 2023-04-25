using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KeralaMiniMart.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;

        public CustomerController(IUserService userService)
        {
            _userService = userService;
        }

        #region Index
        /// <summary>
        /// This method is for index view of customers
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult Index(FilterBase filter)
        {
            ViewBag.Filters = filter;
            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of customers
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(FilterBase filter)
        {
            return ViewComponent("Customer", new { filter, IsPartial = true });
        }
        #endregion

        #region Details
        /// <summary>
        /// This method is for fetching customer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            CustomerDetailsViewModel model = await _userService.GetCustomerDetails(id);
            return View(model);
        }
        #endregion
    }
}
