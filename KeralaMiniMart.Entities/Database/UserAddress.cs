using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string Locality { get; set; }
        public string Pincode { get; set; }
        public bool IsDeleted { get; set; }

        public int? DeliveryLocationId { get; set; }
        public DeliveryLocation DeliveryLocation { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
