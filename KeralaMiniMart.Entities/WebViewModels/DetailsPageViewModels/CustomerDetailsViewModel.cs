using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class CustomerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string RegisteredDate { get; set; }

        public List<OrderListViewModel> OrderList { get; set; }
        public List<UserAddressViewModels> UserAddressList { get; set; }
        public string City { get; set; }
    }

    public class UserAddressViewModels
    {
        public int Number { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string Pincode { get; set; }
        public string MobileNumber { get; set; }
        public string Landmark { get; set; }
    }
}
