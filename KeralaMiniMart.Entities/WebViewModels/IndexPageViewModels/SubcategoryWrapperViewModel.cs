using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class SubcategoryWrapperViewModel
    {
        public List<SubcategoryListViewModel> SubcateogryList { get; set; }
        public PagingData PagingData { get; set; }
        public int TotalCount { get; set; }
    }

    public class SubcategoryListViewModel
    {
        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }
}
