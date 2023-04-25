using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Database
{
    public class NotificationDeliveryLocation
    {
        public int Id { get; set; }

        public int? NotificationId { get; set; }
        public Notification Notification { get; set; }

        public int? DeliveryLocationId { get; set; }
        public DeliveryLocation DeliveryLocation { get; set; }
    }
}
