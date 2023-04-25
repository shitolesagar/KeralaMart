using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IWebCategoryService
    {
        Task<int> AddCategoryAsync(AddCategoryViewModel model, string imageRelativePath);
        Task<int> EditCategoryAsync(int id, AddCategoryViewModel model, string imageRelativePath);
        Task<CategoryWrapperViewModel> GetWrapperForIndexView(FilterBase filter);
        Task<int> DeleteCategory(int id);
        Task<AddCategoryViewModel> getForEditAsync(int id);
        Task<CategoryDetailsViewModel> GetCategoryDetails(int id);
    }
}