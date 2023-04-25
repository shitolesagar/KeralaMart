using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Colors
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HashCode { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
