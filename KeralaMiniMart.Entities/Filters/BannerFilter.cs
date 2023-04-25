using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.Filters
{
    public class BannerFilter: FilterBase
    {
        public bool showExpired { get; set; }
        public bool showInActive { get; set; }
    }
}
