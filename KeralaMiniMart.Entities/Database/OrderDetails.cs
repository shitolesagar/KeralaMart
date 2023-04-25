using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal? OriginalPrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? ColorsId { get; set; }
        public Colors Colors { get; set; }

        public int? SizeId { get; set; }
        public Size Size { get; set; }
    }
}
