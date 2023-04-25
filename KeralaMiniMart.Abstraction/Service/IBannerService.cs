using System.Collections.Generic;
using System.Threading.Tasks;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IBannerService
    {
        Task<int> AddBannerAsync(AddBannerViewModel model, string imageRelativePath);
        Task<BannerWrapperViewModel> GetWrapperForIndexView(BannerFilter filter);
        Task<int> DeleteBanner(int id);
        Task<AddBannerViewModel> getForEditAsync(int id);
        Task<int> EditBannerAsync(int id, AddBannerViewModel model, string imageRelativePath);
        Task<BannerDetailsViewModel> GetBannerDetails(int id);
    }
}