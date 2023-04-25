using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Redirect { get; set; }
        public string Platform { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ImageUrl { get; set; }
        public string NotificationType { get; set; }
        public string Title { get; set; }
        public bool IsShowOnWeb { get; set; }

        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<NotificationDeliveryLocation> NotificationDeliveryLocations { get; set; }
    }
}
