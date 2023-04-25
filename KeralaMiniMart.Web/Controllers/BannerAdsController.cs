using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KeralaMiniMart.Web.Controllers
{
    public class BannerAdsController : Controller
    {
        private readonly IFileServices _fileServices;
        private readonly IBannerService _bannerService;

        public BannerAdsController(IFileServices fileServices, IBannerService bannerService)
        {
            _fileServices = fileServices;
            _bannerService = bannerService;
        }

        #region Index
        /// <summary>
        /// This method is used for index view of banners
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult Index(BannerFilter filter)
        {
            if (filter.showExpired || filter.showInActive)
                ViewBag.IsPartial = true;
            else
                ViewBag.IsPartial = false;
            ViewBag.Filters = filter;
            if (filter.showInActive)
                ViewBag.ddlStausSelected = "InActive";
            else if (filter.showExpired)
                ViewBag.ddlStausSelected = "Expired";
            else
                ViewBag.ddlStausSelected = "Active";
            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of banner
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(BannerFilter filter)
        {
            return ViewComponent("Banner", new { filter, IsPartial = true });
        }
        #endregion

        #region Delete Banner
        /// <summary>
        /// This method is for delete banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id, BannerFilter filter)
        {
            int deletedRows = await _bannerService.DeleteBanner(id);
            if (deletedRows > 0)
                TempData["Message"] = MessageConstants.BannerDeleteSuccessMessage;
            else
                TempData["Message"] = MessageConstants.BannerDeleteFailedMessage;
            return RedirectToAction("Index", new { filter.showExpired, filter.showInActive });
        }
        #endregion

        #region Add
        /// <summary>
        /// This is get method for adding banner
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// This is post method for adding banner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddBannerViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            string relativePath = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.BannerFolder);
            var BannerId = await _bannerService.AddBannerAsync(model, relativePath);
            TempData["Message"] = MessageConstants.BannerAddSuccessMessage;
            return RedirectToAction("details", new { id = BannerId });
        }
        #endregion

        #region Edit
        /// <summary>
        /// This is get method for edit banner
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {

            AddBannerViewModel model = await _bannerService.getForEditAsync(id);
            return View(model);
        }

        /// <summary>
        /// This is post method for edit banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddBannerViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            string relativePath = string.Empty;
            if (model.File != null)
                relativePath = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.BannerFolder);
            await _bannerService.EditBannerAsync(id, model, relativePath);
            TempData["Message"] = MessageConstants.BannerUpdateSuccessMessage;
            return RedirectToAction("details", new { id });
        }
        #endregion

        #region Details
        /// <summary>
        /// This method is for fetching banner details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            BannerDetailsViewModel model = await _bannerService.GetBannerDetails(id);
            return View(model);
        }
        #endregion
    }
}
