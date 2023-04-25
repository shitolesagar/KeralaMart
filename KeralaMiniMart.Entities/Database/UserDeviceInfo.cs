using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Database
{
    public class UserDeviceInfo
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }

        public int? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
