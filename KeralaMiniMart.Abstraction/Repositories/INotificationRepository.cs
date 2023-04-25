using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Task<List<Notification>> GetNotificationList(int skip, int take, string AppId);
        Task<List<Notification>> GetNotificationListForUser(int skip, int take, string AppId, int ApplicationUserId);
        int GetIndexViewTotalCount(NotificationFilter filter);
        Task<List<Notification>> GetIndexViewRecordsAsync(NotificationFilter filter, int skip, int pageSize);
        Notification FindNotificationById(int id);
    }
}
