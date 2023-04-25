using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeralaMiniMart.Repository.Repositories
{
    public class NotificationDeliveryLocationRepository : Repository<NotificationDeliveryLocation>, INotificationDeliveryLocationRepository
    {
        public NotificationDeliveryLocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public List<NotificationDeliveryLocation> GetNotificationDeliveryLocationList(int NotificationId)
        {
            return Set.Where(x => x.NotificationId == NotificationId).ToList();
        }
    }
}
