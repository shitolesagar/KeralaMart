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
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IFileServices _fileServices;
        private readonly ISubcategoryService _filterService;

        public ProductController(IProductService productService, IFileServices fileServices, ISubcategoryService filterService)
        {
            _productService = productService;
            _fileServices = fileServices;
            _filterService = filterService;
        }

        #region Index
        /// <summary>
        /// This method is for index view of product
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(ProductFilter filter)
        {
            List<IdNameViewModel> categoryList = await _productService.GetCategoryListAsync();
            ViewBag.CategoryList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.Filters = filter;
            return View();
        }
        #endregion

        #region Index Partial
        /// <summary>
        /// This method is for partial view of product
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IActionResult IndexPartial(ProductFilter filter)
        {
            return ViewComponent("Product", new { filter, IsPartial = true });
        }
        #endregion

        #region Add Product
        /// <summary>
        /// This is get method for adding product
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Add()
        {
            await GetDropdownData();
            return View();
        }

        /// <summary>
        /// This is post method for adding product
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Add");
            model.MainImageText = await _fileServices.SaveImageAndReturnRelativePath(model.Image, FolderConstants.ProductsFolder);
            if (model.Image1 != null)
                model.Image1RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image1, FolderConstants.ProductsFolder);
            if (model.Image2 != null)
                model.Image2RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image2, FolderConstants.ProductsFolder);
            if (model.Image3 != null)
                model.Image3RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image3, FolderConstants.ProductsFolder);
            if (model.Image4 != null)
                model.Image4RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image4, FolderConstants.ProductsFolder);
            var productId = await _productService.AddProductAsync(model);
            TempData["Message"] = MessageConstants.ProductAddSuccessMessage;
            return RedirectToAction("details", new { id = productId});
        }
        #endregion

        #region Product Details
        /// <summary>
        /// This method is for fetching product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductDetailsViewModel model = await _productService.GetForDetailsAsync(id);
            return View(model);
        }
        #endregion

        #region GetSubcategoryDropList
        /// <summary>
        /// This method is for fetching subcategory list for dropdown
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetSubcategoryDropList(int categoryId)
        {
            List<IdNameViewModel> list = await _filterService.GetSubcategoryListAsync(categoryId);
            return Json(list);
        }
        #endregion

        #region Edit Product
        /// <summary>
        /// This is get method for edit product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            await GetDropdownData();
            AddProductViewModel model = await _productService.GetForEditAsync(id);
            List<IdNameViewModel> FilterList = await _productService.GetFilterListForProductEditAsync(model.CategoryId.Value);
            ViewBag.FiltersList = FilterList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            return View(model);
        }

        /// <summary>
        /// This is post method for edit product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddProductViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("edit", new { id });
            if (model.Image != null)
                model.MainImageText = await _fileServices.SaveImageAndReturnRelativePath(model.Image, FolderConstants.ProductsFolder);
            if (model.Image1 != null)
                model.Image1RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image1, FolderConstants.ProductsFolder);
            if (model.Image2 != null)
                model.Image2RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image2, FolderConstants.ProductsFolder);
            if (model.Image3 != null)
                model.Image3RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image3, FolderConstants.ProductsFolder);
            if (model.Image4 != null)
                model.Image4RelativePath = await _fileServices.SaveImageAndReturnRelativePath(model.Image4, FolderConstants.ProductsFolder);
            await _productService.EditProductSaveAsync(id, model);
            TempData["Message"] = MessageConstants.ProductUpdateSuccessMessage;
            return RedirectToAction("details", new { id  });
        }
        #endregion

        #region Delete Product
        /// <summary>
        /// This is post method for delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            int deletedRows = await _productService.DeleteProduct(id);
            if (deletedRows > 0)
                TempData["Message"] = MessageConstants.ProductDeletedSuccessMessage;
            else
                TempData["Message"] = MessageConstants.ProductDeletionFailedMessage;
            return RedirectToAction("Index");
        }
        #endregion

        #region Private Methods
        public async Task GetDropdownData()
        {
            List<IdNameViewModel> UnitList = await _productService.GetUnitListAsync();
            List<IdNameViewModel> categoryList = await _productService.GetCategoryListAsync();
            List<IdNameViewModel> FilterList = await _productService.GetFilterListAsync();
            List<IdNameViewModel> ColorList = await _productService.GetColorListAsync();
            List<IdNameViewModel> AvailableSize = await _productService.GetAvailableSizeList();
            ViewBag.FiltersList = FilterList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.CategoryList = categoryList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.ColorList = ColorList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.AvaliableSizeList = AvailableSize.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
            ViewBag.UnitList = UnitList.Select(x => new SelectListItem() { Value = x.Id.ToString(), Text = x.Name });
        }
        #endregion
    }
}