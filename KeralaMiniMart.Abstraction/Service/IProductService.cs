using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IProductService
    {
        Task<int> AddProductAsync(AddProductViewModel model);
        Task<ProductWrapperViewModel> GetWrapperForIndexView(ProductFilter filter);
        Task<List<IdNameViewModel>> GetCategoryListAsync();
        Task<List<IdNameViewModel>> GetFilterListAsync();
        Task<List<IdNameViewModel>> GetFilterListForProductEditAsync(int categoryId);
        Task<List<IdNameViewModel>> GetColorListAsync();
        Task<List<IdNameViewModel>> GetAvailableSizeList();
        Task<int> DeleteProduct(int id);
        Task<ProductDetailsViewModel> GetForDetailsAsync(int id);
        Task<AddProductViewModel> GetForEditAsync(int id);
        Task<int> EditProductSaveAsync(int id, AddProductViewModel model);
        Task<List<IdNameViewModel>> GetUnitListAsync();
    }
}