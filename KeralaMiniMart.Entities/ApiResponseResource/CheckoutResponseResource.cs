using KeralaMiniMart.Entities.ApiRequestResource;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiResponseResource
{
    public class AddToCartResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
    }

    //This class is used as a resource to send response of GetCartList Api response
    public class CartResponse
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductImageURL { get; set; }
        public string ProductName { get; set; }
        public string ColorCode { get; set; }
        public string Size { get; set; }
        public string ShortDescription { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableQuantity { get; set; }
    }

    public class OrderDetailModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductImageURL { get; set; }
        public string ProductName { get; set; }
        public string ColorCode { get; set; }
        public string Size { get; set; }
        public string ShortDescription { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }

    }
    public class GetProductForOrderResponse
    {
        public bool error { get; set; }
        public string Message { get; set; }
        public List<OrderDetailModel> data { get; set; }
        public int Count { get; set; }
    }
    

    public class GetCartListResponse
    {
        public bool error { get; set; }
        public string Message { get; set; }
        public List<CartResponse> data { get; set; }
        public int Count { get; set; }
        public string EstimatedDeliveryDate { get; set; }
    }

    public class GetOrderDetailResponse
    {
        public bool error { get; set; }
        public string Message { get; set; }
        public List<CartResponse> data { get; set; }
        public int Count { get; set; }
    }

    public class OrderResponse
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string OrderedDate { get; set; }
        public string DeliveryDay { get; set; }
        public string DeliveredDate { get; set; }
        public int DeliveryStatus { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class GetMyOrdersResponse
    {
        public bool error { get; set; }
        public string Message { get; set; }
        public List<OrderResponse> data { get; set; }
        public int Count { get; set; }
    }

    //This class is used as a resource for sending GetAddressList API response
    public class UsersAddressResponse
    {
        public int Id { get; set; }
        public int? ApplicationUserId { get; set; }
        public string AppId { get; set; }
        public string MobileNumber { get; set; }
        public string PINCode { get; set; }
        public string Address { get; set; }
        public string Landmark {get;set;}
        public string Locality { get; set; }
        public string Name { get; set; }
    }

    public class CommonAddressResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
    }

    public class AddressListResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public List<UsersAddressResponse> data { get; set; }
        public int Count { get; set; }
    }

    public class PriceDetailsResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public PriceDetailsResource data { get; set; }
    }

    public class IncDecProductResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
    }

    public class MyOrderDetails
    {
        public string OrderNumber { get; set; }
        public string OrderedDate { get; set; }
        public string DeliveryDay { get; set; }
        public string DeliveredDate { get; set; }
        public decimal OrderTotal { get; set; }
        public string Address { get; set; }
        public int DeliveryStatus { get; set; }
    }
    public class OrderDetailsResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public OrderDetailsData data { get; set; }
        public int ProductsCount { get; set; }
    }
    public class OrderDetailsData
    {
        public MyOrderDetails OrderDetails { get; set; }
        public List<OrderDetailModel> ProductList { get; set; }
    }
    public class DeliveryDayResponseResource
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public string DeliveryDay { get; set; }
    }

    public class DeliveryLocationListResponseWrapper
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public List<DeliveryLocationListResponse> data { get; set; }
        public int Count { get; set; }
    }

    public class DeliveryLocationListResponse
    {
        public string PinCode { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Area { get; set; }
        public int Id { get; set; }
    }
}
