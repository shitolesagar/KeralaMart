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
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;

        public SubcategoryService(ISubcategoryRepository SubcategoryRepository,ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _subcategoryRepository = SubcategoryRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddSubcategoryAsync(AddSubcategoryViewModel model)
        {
            Subcategory Subcategory = new Subcategory()
            {
                Name = model.SubcategoryName,
                CategoryId = model.CategoryId,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now

            };
            _subcategoryRepository.Add(Subcategory);
            await _unitOfWork.SaveChangesAsync();
            return Subcategory.Id;
        }

        public Task<int> DeleteSubcategory(int id)
        {
            var subcategoryToDelete = _subcategoryRepository.FindById(id);
            _subcategoryRepository.Remove(subcategoryToDelete);
            return _unitOfWork.SaveChangesAsync();
        }

        public async Task<AddSubcategoryViewModel> GetForEditAsync(int id)
        {
            var subcategoryToEdit = await _subcategoryRepository.FindByIdAsync(id);
            if (subcategoryToEdit == null)
                return null;
            AddSubcategoryViewModel model = new AddSubcategoryViewModel()
            {
                ImageUrl = subcategoryToEdit.ImagePath,
                CategoryId = subcategoryToEdit.CategoryId,
                SubcategoryName = subcategoryToEdit.Name
            };
            return model;
        }

        public async Task EditSubcategoryAsync(int id, AddSubcategoryViewModel model)
        {
            var subcategoryToEdit = await _subcategoryRepository.FindByIdAsync(id);
            subcategoryToEdit.Name = model.SubcategoryName;
            subcategoryToEdit.CategoryId = model.CategoryId;
            subcategoryToEdit.ModifiedDateTime = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();
        }

       

        public async Task<List<IdNameViewModel>> GetCategoryList()
        {
            var list = await _categoryRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }

        

        public async Task<SubcategoryWrapperViewModel> GetWrapperForIndexView(SubcategoryFilter filter)
        {
            SubcategoryWrapperViewModel ResponseModel = new SubcategoryWrapperViewModel
            {
                TotalCount = _subcategoryRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Subcategory> list = await _subcategoryRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.SubcateogryList = list.Select((x, index) => new SubcategoryListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Category = x.Category.Name,
                Number = ResponseModel.PagingData.FromRecord + index,
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }

        public async Task<List<IdNameViewModel>> GetSubcategoryListAsync(int categoryId)
        {
            var subcategories = await _subcategoryRepository.GetAllAsync();
            var list = subcategories.Where(x => x.CategoryId == categoryId);
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }

        public async Task<SubcategoryDetailsViewModel> GetSubcategoryDetails(int id)
        {
            SubcategoryDetailsViewModel model;
            var subcategory = await _subcategoryRepository.FindByIdAsync(id,true);
            if (subcategory == null)
                return null;
            model = new SubcategoryDetailsViewModel()
            {
                ImagePath = subcategory.ImagePath,
                Id = subcategory.Id,
                Name = subcategory.Name,
                CategoryName = subcategory.Category.Name,
                CreatedDate = subcategory.CreatedDateTime.ToStringDateTimePattern(),
                ModifiedDate = subcategory.CreatedDateTime.ToStringDateTimePattern() == subcategory.ModifiedDateTime.ToStringDateTimePattern() ? null : subcategory.ModifiedDateTime.ToStringDateTimePattern()
            };
            return model;
        }
    }
}
