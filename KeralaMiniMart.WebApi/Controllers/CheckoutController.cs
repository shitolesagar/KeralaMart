using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.Constant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeralaMiniMart.WebApi.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        #region AddToCart
        [Route("api/Checkout/AddToCart")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddToCart([FromBody] AddToCart request)
        {
            var response = _checkoutService.AddToCart(request);

            if (response.error == true)
            {
                if (response.Message == StringConstants.ItemExist)
                    return Ok(new { statusCode = StringConstants.StatusCode30, message = response.Message });
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region IncDecProductQuantity
        [Route("api/Checkout/IncDecProductQuantity")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult IncDecProductQuantity([FromBody] IncDecProductResource request)
        {
            var response = _checkoutService.IncDecProductQuantity(request);

            if (response.error == true)
            {
                if(response.Message == StringConstants.CantAddMore)
                {
                    return Ok(new { statusCode = StringConstants.StatusCode30, message = response.Message });
                }
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region GetCartList

        [Route("api/Checkout/GetCartList")]
        [HttpGet]
        public async Task<IActionResult> GetCartList(string AppId, int ApplicationUserId)
        {

            var response = await _checkoutService.GetCartList(AppId, ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data, count = response.Count });
        }
        #endregion

        #region AddNewAddress
        [Route("api/Checkout/AddNewAddress")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddNewAddress([FromBody] UsersAddress request)
        {
            var response = _checkoutService.AddAddress(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region EditAddress
        [Route("api/Checkout/EditAddress")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult EditAddress([FromBody] UsersAddress request)
        {
            var response = _checkoutService.EditAddress(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region DeleteAddress
        [Route("api/Checkout/DeleteAddress")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult DeleteAddress([FromBody] DeleteAddressResource request)
        {
            var response = _checkoutService.DeleteAddress(request);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region GetAddressList

        [Route("api/Checkout/GetAddressList")]
        [HttpGet]
        public async Task<IActionResult> GetAddressList(string AppId, int ApplicationUserId)
        {
            var response = await _checkoutService.GetAddressList(AppId, ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data, Count = response.Count });
        }
        #endregion

        #region GetTotalPrice

        [Route("api/Checkout/GetTotalPrice")]
        [HttpGet]
        public async Task<IActionResult> GetTotalPrice(string AppId, int ApplicationUserId)
        {
            var response = await _checkoutService.GetPriceDetails(AppId, ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data });
        }
        #endregion

        #region PlaceOrder

        [Route("api/Checkout/PlaceOrder")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderResource request)
        {
            var response = await _checkoutService.PlaceOrder(request);

            if (response.error == true)
            {
                if (response.Message == StringConstants.RateChanged)
                {
                    return Ok(new { statusCode = StringConstants.StatusCode30, message = response.Message });
                }
                else if (response.Message == StringConstants.OutOfStock)
                {
                    return Ok(new { statusCode = StringConstants.StatusCode30, message = response.Message });
                }
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message, response.MessageError });

            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, OrderId = response.OrderId, DeliveryDay = response.DeliveryDate, OrderNumber = response.OrderNumber });
        }
        #endregion

        #region GetMyOrders

        [Route("api/Checkout/GetMyOrders")]
        [HttpGet]
        public async Task<IActionResult> GetMyOrders(string AppId, int ApplicationUserId)
        {

            var response = await _checkoutService.GetMyOrders(AppId, ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data, count = response.Count });
        }
        #endregion

        #region GetOrderDetails

        [Route("api/Checkout/GetOrderDetails")]
        [HttpGet]
        public async Task<IActionResult> GetOrderDetails(string AppId, int OrderId)
        {

            var response = await _checkoutService.GetOrderDetails(AppId, OrderId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data, ProductCount = response.ProductsCount });
        }
        #endregion

        #region UpdateTransactionId

        [Route("api/Checkout/UpdateTransactionId")]
        [HttpPost]
        public async Task<IActionResult> UpdateTransactionId([FromBody]UpdateTransactionIdResource request)
        {

            var response = await _checkoutService.UpdateTransactionId(request.OrderId, request.TransactionId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }
        #endregion

        #region AddToken

        [Route("api/Checkout/AddToken")]
        [HttpPost]
        public IActionResult AddToken([FromBody] AddTokenResource request)
        {

            var response = _checkoutService.AddToken(request.Token, request.ApplicationUserId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }
        #endregion

        #region GetProductsForOrder [Depricated]

        [Route("api/Checkout/GetProductsForOrder")]
        [HttpGet]
        public async Task<IActionResult> GetProductsForOrder(int OrderId)
        {

            var response = await _checkoutService.GetProductsForOrder(OrderId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data });
        }
        #endregion

        #region GetDeliveryDay

        [Route("api/Checkout/GetDeliveryDay")]
        [HttpGet]
        public IActionResult GetDeliveryDay(int AddressId)
        {

            var response = _checkoutService.GetDeliveryDay(AddressId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, DeliveryDay = response.DeliveryDay });
        }
        #endregion

        #region RemoveFromCart
        [Route("api/Checkout/RemoveFromCart")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RemoveFromCart([FromBody] RemoveFromCartResource request)
        {
            var response = _checkoutService.RemoveFromCart(request.ApplicationUserId, request.ProductId, request.AppId);

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message });
        }

        #endregion

        #region GetDeliveryChart
        [Route("api/Checkout/GetDeliveryChart")]
        [HttpGet]
        public async Task<IActionResult> GetDeliveryChart()
        {
            var response = await _checkoutService.GetDeliveryLocations();

            if (response.error == true)
            {
                return Ok(new { statusCode = StringConstants.StatusCode20, message = response.Message });
            }
            return Ok(new { statusCode = StringConstants.StatusCode10, message = response.Message, data = response.data });
        }
        #endregion
    }
}