using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Database
{
    public class OrderPaymentStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
