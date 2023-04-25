using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface ICategoryService
    {
         Task<ApplicationThemeResponse> GetAppTheme();

         VersionResponse GetVersion(string AppId);

        Task<BannerResponse> GetBanner(int take, string AppId);

        Task<CategoryResponse> GetCategory(int take, string AppId);

        Task<NotificationResponse> GetNotification(int PageIndex,int PageSize, string AppId, int ApplicationUserId);

        Task<FilterResponse> GetFilter(int CategoryId, string AppId);

        Task<ProductResponse> GetProductListForSearch(int take, string AppId, string Search);

        ProductDetailsResponse GetProductDetail(int Id, string AppId);

        ProductListResponse GetAllProductForCategory(int categoryId, string AppId, int skip, int take);

        ProductListResponse GetAllExclusiveProductForCategory(int categoryId, string AppId, int skip, int take);

        ProductListResponse GetRecommendedProduct(int categoryId, int SubCategoryId, int ProductId, string AppId, int skip, int take);

        ProductListResponse ApplyAllFilters(ApplyFiltersResource request);
    }
}
