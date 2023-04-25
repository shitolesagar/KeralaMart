using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KeralaMiniMart.Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IProductService _productService;
        private readonly IFileServices _fileServices;

        public NotificationController(INotificationService notificationService, IProductService productService,  IFileServices fileServices)
        {
            _notificationService = notificationService;
            _fileServices = fileServices;
            _productService = productService;
        }

        #region Index
        /// <summary>
        /// This method is for index view of notification
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult Index(NotificationFilter filter)
        {
            ViewBag.Filters = filter;
            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of notification
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(NotificationFilter filter)
        {
            return ViewComponent("Notification", new { filter, IsPartial = true });
        }
        #endregion

        #region Add
        /// <summary>
        /// This is get method for add notification
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            await GetDropdownData();
            AddNotificationViewModel model = new AddNotificationViewModel()
            {
                Type = "Text"
            };
            List<IdNameViewModel> DeliveryLocationList = _notificationService.GetDeliveryLocationList();
            ViewBag.DeliveryLocationList = DeliveryLocationList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return View(model);
        }

        /// <summary>
        /// This is post method for add notification
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddNotificationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (model.File != null)
                model.ImageUrl = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.NotificationFolder);
            int notificationId = await _notificationService.AddNotificationAsync(model);
            TempData["Message"] = MessageConstants.NotificationAddSuccessMessage;
            return RedirectToAction("details", new { id = notificationId });
        }
        #endregion

        #region Edit Get
        /// <summary>
        /// This is get method for edit notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            await GetDropdownData();
            AddNotificationViewModel model =  _notificationService.GetForEditAsync(id);
            List<IdNameViewModel> DeliveryLocationList = _notificationService.GetDeliveryLocationList();
            ViewBag.DeliveryLocationList = DeliveryLocationList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return View(model);
        }
        #endregion

        #region Delete Notification
        /// <summary>
        /// This method is for deleting notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int deletedRows = await _notificationService.DeleteNotification(id);
            if (deletedRows > 0)
                TempData["Message"] = MessageConstants.NotificationDeleteSuccessMessage;
            else
                TempData["Message"] = MessageConstants.NotificationDeleteFailedMessage;
            return RedirectToAction("Index");
        }
        #endregion

        #region Details
        /// <summary>
        /// This method is for fetchinf notification details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            NotificationDetailsViewModel model = _notificationService.GetNotificationDetails(id);
            return View(model);
        }
        #endregion

        #region Private Methods
        public async Task GetDropdownData()
        {
            List<IdNameViewModel> categoryList = await _productService.GetCategoryListAsync();
            ViewBag.CategoryList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }
        #endregion
    }
}
