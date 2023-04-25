using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Filters
{
    public class FilterBase
    {
        public string search { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
