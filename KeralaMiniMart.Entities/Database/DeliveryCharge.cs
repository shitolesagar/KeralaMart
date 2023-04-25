using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Database
{
    public class DeliveryCharge
    {
        public int Id { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Charge { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
