

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.Filters;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Entities.WebViewModels.DetailsPageViewModels;
using KeralaMiniMart.Service.ExtensionMethods;

namespace KeralaMiniMart.Service
{
    public class OrderService : IOrderService
    {
        private readonly ISmsService _smsService;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDeliveryLocationRepository _deliveryLocationRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private IUnitOfWork _unitOfWork;
        private readonly IOrderDeliveryStatusRepository _deliveryStatusRepository;

        public OrderService(ISmsService smsService, IOrderRepository orderRepository, IProductRepository productRepository, IDeliveryLocationRepository deliveryLocationRepository, IOrderDetailsRepository orderDetailsRepository, IUnitOfWork unitOfWork, IOrderDeliveryStatusRepository orderDeliveryStatusRepository)
        {
            _smsService = smsService;
            _orderRepository = orderRepository;
            _deliveryLocationRepository = deliveryLocationRepository;
            _unitOfWork = unitOfWork;
            _deliveryStatusRepository = orderDeliveryStatusRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _productRepository = productRepository;
        }

        #region GetWrapperForIndexView
        public async Task<OrderWrapperViewModel> GetWrapperForIndexView(OrderFilter filter)
        {
            OrderWrapperViewModel ResponseModel = new OrderWrapperViewModel
            {
                TotalCount = _orderRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Order> list = await _orderRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.OrderList = list.Select((x, index) => new OrderListViewModel
            {
                Id = x.Id,
                OrderNumber = x.OrderNumber,
                UserName = x.ApplicationUser.Name + string.Format(" ({0})", x.ApplicationUser.MobileNumber),
                Status = x.DeliveryStatus.Status,
                CreatedDate = x.CreatedDate.ToStringShortDayOfWeekPattern(),
                DeliveryDay = x.EstimatedDeliveryDate.ToStringShortDayOfWeekPattern(),
                Number = ResponseModel.PagingData.FromRecord + index,
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }
        #endregion

        #region GetAllDeliveryStatusAsync
        public async Task<List<IdNameViewModel>> GetAllDeliveryStatusAsync()
        {
            var list = await _deliveryStatusRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Status }).ToList();
            return responseList;
        }
        #endregion

        #region GetOrderDetails
        public async Task<OrderDetailsViewModel> GetOrderDetails(int id)
        {
            OrderDetailsViewModel model;
            Order order = await _orderRepository.GetOrderDetails(id);
            if (order == null)
                return null;
            var deliveryDayRecord = _deliveryLocationRepository.findByLocalityAndPincode(order.UserAddress.Locality, order.UserAddress.Pincode);
            model = new OrderDetailsViewModel()
            {
                Id = order.Id,
                SubTotal = Math.Round((order.MRP - order.DeliveryCharges), 2),
                DiscountedPrice = Math.Round(order.DiscountPrice, 2),
                TotalPrice = order.MRP,//  Math.Round(order.TotalPrice, 2) ,
                GSTPrice = Math.Round(order.GSTPrice, 2),
                DeliveryCharges = Math.Round(order.DeliveryCharges, 2),
                OrderNumber = order.OrderNumber,
                CreatedDate = order.CreatedDate.ToStringDayOfWeekPattern(),
                ApplicationUser = order.ApplicationUser.Name,
                Email = order.ApplicationUser.Email,
                MobileNumber = order.ApplicationUser.MobileNumber,
                ClientComment = string.IsNullOrEmpty(order.Comments) ? "NA" : order.Comments,
                DeliveryStatusId = order.DeliveryStatusId,
                PaymentStatus = order.PaymentStatus.Status,
                UserId = order.ApplicationUserId.ToString(),
                ShippingAddress = new UserAddressViewModels()
                {
                    Address = order.UserAddress.Address,
                    Locality = order.UserAddress.Locality,
                    MobileNumber = order.UserAddress.MobileNumber,
                    Pincode = order.UserAddress.Pincode,
                    Landmark = order.UserAddress.Landmark
                }
            };

            model.DeliveryDay = order.EstimatedDeliveryDate.ToStringShortDayOfWeekPattern();
            List<OrderDetail> Orders = _orderDetailsRepository.GetAllItemsForOrder(id);
            model.OrderDetails = Orders.Select((x, index) => new OrderDetailsModel()
            {
                ProductId = x.ProductId,
                ItemCount = x.Quantity,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                Brand = x.Product.Brand,
                Number = (index + 1).ToString(),
                DiscountedPrice = Math.Round(x.DiscountedPrice, 2),
                TotalPrice = Math.Round((x.DiscountedPrice * x.Quantity), 2),
                OrderedUnits = x.Product.Quantity + " " + x.Product.Unit.UnitName
            }).ToList();
            return model;
        }
        #endregion

        #region UpdateStatus
        public async Task<string> UpdateStatus(UpdateStatusResource filter)
        {
            try
            {
                Order order = await _orderRepository.GetOrderDetails(filter.Id);

                if (filter.DeliveryStatusId == 5)
                {
                    UpdateProductsStock(filter.Id, false);
                    order.DeliveredSmsResponse = _smsService.SendSms(order.UserAddress.MobileNumber, _smsService.CancelDeliveryMessage(order.OrderNumber));
                }
                else if (order.DeliveryStatusId == 5 && filter.DeliveryStatusId !=5)
                {
                    UpdateProductsStock(filter.Id, true);
                }

                order.DeliveryStatusId = filter.DeliveryStatusId;
                #region Sending SMS and Updating Delivery Date  
                if (filter.DeliveryStatusId == 4)
                {
                    order.DeliveredDate = DateTime.Now;
                    // send Delivery SMS
                    order.DeliveredSmsResponse = _smsService.SendSms(order.UserAddress.MobileNumber, _smsService.CreateDeliveryMessage(order.OrderNumber));
                }
                else
                    order.DeliveredDate = null;
                #endregion
                await _unitOfWork.SaveChangesAsync();
                return StringConstants.Success;
            }
            catch
            {
                return StringConstants.ServerError;
            }
        }
        #endregion

        #region Private Methods

        #region UpdateProductsStock
        /// <summary>
        /// This method reverts products quantity if order is not delivered
        /// </summary>
        /// <param name="OrderId"></param>
        private void UpdateProductsStock(int OrderId, bool isDecrement)
        {
            List<OrderDetail> Records = _orderDetailsRepository.GetAllItemsForOrder(OrderId);
            foreach (var rec in Records)
            {
                Product pro = _productRepository.FindById(rec.ProductId);
                if (pro.IsAutomateStockMaintainance == true)
                {
                    if (isDecrement)
                        pro.AvailableQuantity = pro.AvailableQuantity - rec.Quantity;
                    else
                        pro.AvailableQuantity = pro.AvailableQuantity + rec.Quantity;

                }

            }
        }
        #endregion

        #endregion
    }
}
