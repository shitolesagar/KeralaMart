using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class CustomerWrapperViewModel
    {
        public List<CustomerListViewModel> UserList { get; set; }
        public PagingData PagingData { get; set; }
        public int TotalCount { get; set; }
    }

    public class CustomerListViewModel
    {
        public int Number { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreatedDate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
