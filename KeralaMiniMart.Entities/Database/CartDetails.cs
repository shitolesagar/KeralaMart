using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class CartDetails
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public int ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? ColorsId { get; set; }
        public Colors Colors { get; set; }

        public int? SizeId { get; set; }
        public Size SIze { get; set; }
    }
}
