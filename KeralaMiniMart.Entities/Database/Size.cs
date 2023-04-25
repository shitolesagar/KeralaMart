using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Size
    {
        public int Id { get; set; }
        public string ProductSize { get; set; }
        public string Unit { get; set; }

        public ICollection<ProductSize> ProductSizes { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
