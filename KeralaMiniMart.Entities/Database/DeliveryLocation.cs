using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class DeliveryLocation
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Area { get; set; }
        public string Pincode { get; set; }
        public string Time { get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; }
        public ICollection<NotificationDeliveryLocation> NotificationDeliveryLocations { get; set; }
    }
}
