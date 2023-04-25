using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels.DetailsPageViewModels
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal DiscountedPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal GSTPrice { get; set; }
        public decimal DeliveryCharges { get; set; }
        public string OrderNumber { get; set; }
        public string CreatedDate { get; set; }
        public string ApplicationUser { get; set; }
        public string UserId { get; set; }
        public int DeliveryStatusId { get; set; }
        public string PaymentStatus { get; set; }
        public string DeliveryDay { get; set; }
        public UserAddressViewModels ShippingAddress { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
        public string ClientComment { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }
    public class EditOrderDetailsModel
    {
        public int OrderId { get; set; }
        public int DeliveryStatusId { get; set; }
        public int PaymentStatusId { get; set; }
    }
    public class OrderDetailsModel
    {
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int ItemCount { get; set; }
        public string OrderedUnits { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
