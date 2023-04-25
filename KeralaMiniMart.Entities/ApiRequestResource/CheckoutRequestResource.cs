using KeralaMiniMart.Entities.ApiResponseResource;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiRequestResource
{
    public class AddToCart
    {
        public int ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public int ColorsId { get; set; }
        public int SizeId { get; set; }
        public string AppId { get; set; }
    }

    public class AddToCartResourceWrapper
    {
        public List<CartResponse> CartList { get; set; }
        public int TotalCount { get; set; }
    }

    //This class is used as a resource for adding the new address to database
    public class UsersAddress
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public string AppId { get; set; }
        public string MobileNumber { get; set; }
        public string PINCode { get; set; }
        public string Address { get; set; }
        public string Landmark { get; set; }
        public string Locality { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? PinCodeId { get; set; }
    }

    public class AddressResourceWrapper
    {
        public List<UsersAddressResponse> AddressList { get; set; }
        public int TotalCount { get; set; }
    }

    public class PriceDetailsResource
    {
        public int MRP { get; set; }
        public int ProductDiscounts { get; set; }
        public int DeliveryCharges { get; set; }
        public int SubTotal { get; set; }
    }

    public class OrderDetailsResource
    {
        public int ProductDetailId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public int ColorsId { get; set; }
        public int SizeId { get; set; }
    }

    public class PlaceOrderResource
    {
        public int MRP { get; set; }
        public int ProductsDiscount { get; set; }
        public int GST { get; set; }
        public int DeliveryCharges { get; set; }
        public int SubTotal { get; set; }
        public int AddressId { get; set; }
        public int ApplicationUserId { get; set; }
        public string AppId { get; set; }
        public string Comments { get; set; }
        public List<OrderDetailsResource> OrderDetails { get; set; }
    }

    public class AddTokenResource
    {
        public string Token { get; set; }
        public int? ApplicationUserId { get; set; }
    }

    public class UpdateTransactionIdResource
    {
        public int OrderId { get; set; }
        public string TransactionId { get; set; }
    }

    public class IncDecProductResource
    {
        public int ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string AppId { get; set; }
    }

    public class DeleteAddressResource
    {
        public int Id { get; set; }
    }

    public class RemoveFromCartResource
    {
        public int ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public string AppId { get; set; }
    }
}
