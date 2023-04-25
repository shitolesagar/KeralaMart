using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface INotificationDeliveryLocationRepository : IRepository<NotificationDeliveryLocation>
    {
        List<NotificationDeliveryLocation> GetNotificationDeliveryLocationList(int NotificationId);
    }
}
