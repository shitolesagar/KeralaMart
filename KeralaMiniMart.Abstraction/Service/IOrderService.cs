using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Entities.WebViewModels.DetailsPageViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface IOrderService
    {
        Task<OrderWrapperViewModel> GetWrapperForIndexView(OrderFilter filter);
        Task<List<IdNameViewModel>> GetAllDeliveryStatusAsync();
        Task<OrderDetailsViewModel> GetOrderDetails(int id);
        Task<string> UpdateStatus(UpdateStatusResource filter);
    }
}