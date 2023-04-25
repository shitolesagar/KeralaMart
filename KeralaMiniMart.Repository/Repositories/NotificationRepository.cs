using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<Notification>> GetIndexViewRecordsAsync(NotificationFilter filter, int skip, int pageSize)
        {
            var query = Set.Where(x => x.IsShowOnWeb == true).AsQueryable();
            if (!string.IsNullOrEmpty(filter.search))
                query = query.Where(x => x.Title.ToLower().Contains(filter.search.ToLower()));
            if (!string.IsNullOrEmpty(filter.Type))
            {
                query = query.Where(x => x.NotificationType.ToLower() == filter.Type.ToLower());
            }
            return query.OrderByDescending(x => x.CreatedDateTime).Skip(skip).Take(pageSize).ToListAsync();
        }

        public int GetIndexViewTotalCount(NotificationFilter filter)
        {
            var query = Set.Where(x => x.IsShowOnWeb == true).AsQueryable();
            if (!string.IsNullOrEmpty(filter.search))
                query = query.Where(x => x.Title.ToLower().Contains(filter.search.ToLower()));
            if (!string.IsNullOrEmpty(filter.Type))
            {
                query = query.Where(x => x.NotificationType.ToLower() == filter.Type.ToLower());
            }
            return query.Count();
        }

        public Task<List<Notification>> GetNotificationList(int skip, int take, string AppId)
        {
            return Set.Include(x => x.Category).Where(x => x.ApplicationUserId == null && x.NotificationDeliveryLocations.Count()==0).OrderByDescending(x => x.CreatedDateTime).Skip(skip).Take(take).ToListAsync();
        }

        public Task<List<Notification>> GetNotificationListForUser(int skip, int take, string AppId, int ApplicationUserId)
        {
            return Set.Include(x => x.Category).Where(x => (x.ApplicationUserId == null && x.NotificationDeliveryLocations.Count() == 0) || x.ApplicationUserId == ApplicationUserId).OrderByDescending(x => x.CreatedDateTime).Skip(skip).Take(take).ToListAsync();
        }

        public Notification FindNotificationById(int id)
        {
            return Set.Include(x => x.Category).Include(x => x.NotificationDeliveryLocations).ThenInclude(y => y.DeliveryLocation).Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
