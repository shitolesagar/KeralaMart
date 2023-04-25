using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class ProductWrapperViewModel
    {
        public List<ProductListViewModel> ProductList { get; set; }
        public PagingData PagingData { get; set; }
        public int TotalCount { get; set; }
    }

    public class ProductListViewModel
    {
        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string PublishStatus { get; set; }
        public string FilterName { get; set; }
    }
}
