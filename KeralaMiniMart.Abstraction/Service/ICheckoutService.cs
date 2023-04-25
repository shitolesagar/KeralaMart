using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Service
{
    public interface ICheckoutService
    {
        AddToCartResponse AddToCart(AddToCart request);

        IncDecProductResponse IncDecProductQuantity(IncDecProductResource request);

        Task<GetCartListResponse> GetCartList(string AppId, int ApplicationUserId);

        Task<GetMyOrdersResponse> GetMyOrders(string AppId, int ApplicationUserId);

        CommonAddressResponse AddAddress(UsersAddress request);

        CommonAddressResponse EditAddress(UsersAddress request);

        CommonAddressResponse DeleteAddress(DeleteAddressResource request);

        Task<AddressListResponse> GetAddressList(string AppId, int ApplicationUserId);

        Task<PriceDetailsResponse> GetPriceDetails(string AppId, int ApplicationUserId);

        Task<PlaceOrderResponse> PlaceOrder(PlaceOrderResource request);

        Task<OrderDetailsResponse> GetOrderDetails(string AppId, int OrderId);

        Task<CommonResponse> UpdateTransactionId(int OrderId, string TransactionId);

        CommonResponse AddToken(string Token, int? ApplicationUserId);

        Task<GetProductForOrderResponse> GetProductsForOrder(int OrderId);

        DeliveryDayResponseResource GetDeliveryDay(int AddressId);
        CommonResponse RemoveFromCart(int applicationUserId, int productId, string appId);

        Task<DeliveryLocationListResponseWrapper> GetDeliveryLocations();
    }
}
