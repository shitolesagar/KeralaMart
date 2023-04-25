using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class ProductSize
    {
        public int Id { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
