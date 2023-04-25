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
    
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _subcategoryService;
        private readonly IFileServices _fileServices;

        public SubcategoryController(ISubcategoryService SubcategoryService, IFileServices fileServices)
        {
            _subcategoryService = SubcategoryService;
            _fileServices = fileServices;
        }

        #region Index
        /// <summary>
        /// This method is for fetching list of Subcategories
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(SubcategoryFilter filter)
        {
            ViewBag.Filters = filter;
            List<IdNameViewModel> categoryList = await _subcategoryService.GetCategoryList();
            ViewBag.CategoriesList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });

            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of subcategories
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(SubcategoryFilter filter)
        {
            return ViewComponent("Subcategory", new { filter, IsPartial = true });
        }
        #endregion

        #region Add Subcategory
        /// <summary>
        /// This is get method for adding subcategory
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            List<IdNameViewModel> categoryList = await _subcategoryService.GetCategoryList();
            ViewBag.CategoriesList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return View();
        }

        /// <summary>
        /// This is post method for adding subcategory
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddSubcategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Add");

            var subcategoryId = await _subcategoryService.AddSubcategoryAsync(model);
            TempData["Message"] = MessageConstants.SubcategoryAddSuccessMessage;
            return RedirectToAction("details", new { id= subcategoryId });
        }
        #endregion

        #region Delete Subcategory
        /// <summary>
        /// This method is for deleting subcategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int deletedRows = await _subcategoryService.DeleteSubcategory(id);
            if (deletedRows > 0)
                TempData["Message"] = MessageConstants.SubcategoryDeleteSuccessMessage;
            else
                TempData["Message"] = MessageConstants.SubcategoryDeleteFailedMessage;
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit Subcategory
        /// <summary>
        /// This is get method for editing subcategory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            AddSubcategoryViewModel model = await _subcategoryService.GetForEditAsync(id);
            if (model == null)
                return RedirectToAction("Index");
            List<IdNameViewModel> categoryList = await _subcategoryService.GetCategoryList();
            ViewBag.CategoriesList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return View(model);
        }

        /// <summary>
        /// This is post method for editing subcategory
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddSubcategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            string relativePath = string.Empty;
            if (model.File != null)
                relativePath = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.SubcategoriesFolder);
            await _subcategoryService.EditSubcategoryAsync(id, model);
            TempData["Message"] = MessageConstants.SubcategoryUpdateSuccessMessage;
            return RedirectToAction("details", new { id});
        }
        #endregion

        #region Subcategory Details
        /// <summary>
        /// This method is for fetching subcategory details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            SubcategoryDetailsViewModel model = await _subcategoryService.GetSubcategoryDetails(id);
            return View(model);
        }
        #endregion
    }
}
