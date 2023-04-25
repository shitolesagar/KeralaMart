using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Filters
{
    public class ProductFilter: FilterBase
    {
        public int CategoryId { get; set; }
        public int FilterId { get; set; }
        public string PublishStatus { get; set; }
    }
}
