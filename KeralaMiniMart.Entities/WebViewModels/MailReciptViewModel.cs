using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class MailReciptViewModel
    {
        public string OrderNumber { get; set; }
        public string OrderDateTime { get; set; }
        public CustomerDetails CustomerDetails { get; set; } = new CustomerDetails();
        public FinalAmountDetails FinalAmountDetails { get; set; } = new FinalAmountDetails();
        public List<OrderProductDetails> ProductList { get; set; } = new List<OrderProductDetails>();
        public string CustomerComment { get; set; }
    }

    public class CustomerDetails
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public CustomerAddress Address { get; set; } = new CustomerAddress();
    }

    public class CustomerAddress
    {
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string AreaPincode { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class FinalAmountDetails
    {
        public string SubTotal { get; set; }
        public string ProductDiscount { get; set; }
        public string DeliveryCharges { get; set; }
        public string TotalPrice { get; set; }
    }

    public class OrderProductDetails
    {
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public int Id { get; set; }
    }
}
