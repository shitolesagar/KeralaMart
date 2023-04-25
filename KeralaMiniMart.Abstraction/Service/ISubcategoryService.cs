using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface ISubcategoryService
    {
        Task<int> AddSubcategoryAsync(AddSubcategoryViewModel model);
        Task<SubcategoryWrapperViewModel> GetWrapperForIndexView(SubcategoryFilter filter);
        Task<List<IdNameViewModel>> GetCategoryList();
        Task<int> DeleteSubcategory(int id);
        Task<AddSubcategoryViewModel> GetForEditAsync(int id);
        Task EditSubcategoryAsync(int id, AddSubcategoryViewModel model);
        Task<List<IdNameViewModel>> GetSubcategoryListAsync(int categoryId);
        Task<SubcategoryDetailsViewModel> GetSubcategoryDetails(int id);
    }
}