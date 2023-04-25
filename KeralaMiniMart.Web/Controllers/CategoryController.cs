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
    public class CategoryController : Controller
    {
        private readonly IWebCategoryService _categoryService;
        private readonly IFileServices _fileServices;

        public CategoryController(IWebCategoryService categoryService, IFileServices fileServices)
        {
            _categoryService = categoryService;
            _fileServices = fileServices;
        }

        #region Index
        /// <summary>
        /// This method is for index view of category
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
        /// This method is for partial view of category
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(FilterBase filter)
        {
            return ViewComponent("Category", new { filter, IsPartial=true });
        }
        #endregion

        #region Add
        /// <summary>
        /// This is get method for adding category
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// This is post method for adding category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            string relativePath = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.CategoriesFolder);
            var categoryId = await _categoryService.AddCategoryAsync(model, relativePath);
            TempData["Message"] = MessageConstants.CategoryAddSuccessMessage;
            return RedirectToAction("details", new { id = categoryId });
        }
        #endregion

        #region Delete
        /// <summary>
        /// This method is for deleting category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int deletedRows = await _categoryService.DeleteCategory(id);
            if(deletedRows >0 )
                TempData["Message"] = MessageConstants.CategoryDeleteSuccessMessage;
            else
                TempData["Message"] = MessageConstants.CategoryDeleteFailedMessage;
            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        /// <summary>
        /// This is get method for editing category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit (int id)
        {
            
            AddCategoryViewModel model = await _categoryService.getForEditAsync(id);
            return View(model);
        }

        /// <summary>
        /// This is post method for editing category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            string relativePath = string.Empty;
            if (model.File != null)
                 relativePath = await _fileServices.SaveImageAndReturnRelativePath(model.File, FolderConstants.CategoriesFolder);
            await _categoryService.EditCategoryAsync(id, model, relativePath);
            TempData["Message"] = MessageConstants.CategoryUpdateSuccessMessage;
            return RedirectToAction("details", new { id});
        }
        #endregion

        #region Details
        /// <summary>
        /// This method is for fetching category details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            CategoryDetailsViewModel model = await _categoryService.GetCategoryDetails(id);
            return View(model);
        }
        #endregion
    }
}
