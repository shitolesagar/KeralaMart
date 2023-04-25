using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class BannerWrapperViewModel
    {
        public List<BannerListViewModel> BannerList { get; set; }
        public PagingData PagingData { get; set; }
        public int TotalCount { get; set; }
    }

    public class BannerListViewModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public string ImagePath { get; set; }
        public string CreatedDate { get; set; }
        public string ExpireDate { get; set; }
        public int Number { get; set; }
        public bool IsExpired { get; set; }
    }
}
