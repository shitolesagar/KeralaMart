using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;

namespace KeralaMiniMart.Service
{
    public class WebCategoryService : IWebCategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;

        public WebCategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddCategoryAsync(AddCategoryViewModel model, string imageRelativePath)
        {
            var currentTime = DateTime.Now;
            Category category = new Category()
            {
                Image = imageRelativePath,
                Name = model.CategoryName,
                ShortDescription = model.Description,
                CreatedDateTime = currentTime,
                ModifiedDateTime = currentTime
            };
            _categoryRepository.Add(category);
             await _unitOfWork.SaveChangesAsync();
            return category.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            Category category = _categoryRepository.FindCategoryWithoutProducts(id);
            int ProductNotDeletedCount = category.Products.Where(x => x.IsDeleted == false).Count();
            if (ProductNotDeletedCount > 0)
                return 0;
            else
            {
                _categoryRepository.Remove(category);
                var result = await _unitOfWork.SaveChangesAsync();
                return result;
            }
        }

        public async Task<int> EditCategoryAsync(int id, AddCategoryViewModel model, string imageRelativePath)
        {
            Category toUpdate = await _categoryRepository.FindByIdAsync(id);
            toUpdate.Name = model.CategoryName;
            toUpdate.ShortDescription = model.Description;
            toUpdate.ModifiedDateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(imageRelativePath))
            {
                toUpdate.Image = imageRelativePath;
            }
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<CategoryDetailsViewModel> GetCategoryDetails(int id)
        {
            CategoryDetailsViewModel model;
            var category = await _categoryRepository.FindByIdAsync(id);
            if (category == null)
                return null;
            model = new CategoryDetailsViewModel()
            {
                ImagePath = category.Image,
                Id = category.Id,
                Name = category.Name,
                Description = category.ShortDescription,
                CreatedDate = category.CreatedDateTime.ToStringDateTimePattern(),
                ModifiedDate = category.CreatedDateTime.ToStringDateTimePattern() == category.ModifiedDateTime.ToStringDateTimePattern() ? null: category.ModifiedDateTime.ToStringDateTimePattern()
            };
            return model;
        }

        public async Task<AddCategoryViewModel> getForEditAsync(int id)
        {
            var databaseModel = await _categoryRepository.FindByIdAsync(id);
            AddCategoryViewModel model = new AddCategoryViewModel()
            {
                CategoryName = databaseModel.Name,
                ImageUrl = databaseModel.Image,
                Description = databaseModel.ShortDescription
            };
            return model;
        }

        public async Task<CategoryWrapperViewModel> GetWrapperForIndexView(FilterBase filter)
        {
            CategoryWrapperViewModel ResponseModel = new CategoryWrapperViewModel
            {
                TotalCount = _categoryRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Category> list = await _categoryRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.CategoryList = list.Select((x, index) => new CategoryListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                ImagePath = x.Image,
                Number = ResponseModel.PagingData.FromRecord + index,
                Description = x.ShortDescription
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }
    }
}
