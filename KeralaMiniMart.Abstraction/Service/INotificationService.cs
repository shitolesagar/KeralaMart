using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface INotificationService
    {
        Task<int> AddNotificationAsync(AddNotificationViewModel model);
        Task<int> AddNotificationAsync(AddNotificationViewModel model, int ApplicationUserId);
        Task<NotificationWrapperViewModel> GetWrapperForIndexView(NotificationFilter filter);
        Task<int> DeleteNotification(int id);
        NotificationDetailsViewModel GetNotificationDetails(int id);
        List<IdNameViewModel> GetDeliveryLocationList();
        AddNotificationViewModel GetForEditAsync(int id);
    }
}