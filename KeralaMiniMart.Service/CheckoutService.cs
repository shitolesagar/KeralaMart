using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Entities.WebViewModels;
using KeralaMiniMart.Service.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Service
{
    public class CheckoutService : ICheckoutService
    {
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private IEmailService _emailService;
        private IProductImagesRepository _productImagesRepository;
        private IUserAddressRepository _userAddressRepository;
        private IOrderRepository _orderRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IApplicationUserRepository _applicationUserRepository;
        private IForgotPasswordRepository _forgotPasswordRepository;
        private IAppThemeRepository _appThemeRepository;
        private IVersionControlRepository _versionControlRepository;
        private IBannerRepository _bannerRepository;
        private INotificationRepository _notificationRepository;
        private ISubcategoryRepository _filterRepository;
        private IProductRepository _productRepository;
        private ICartDetailsRepository _cartDetailsRepository;
        private IDelivery_chargeRepository _delivery_chargeRepository;
        private IUserDeviceInfoRepository _userDeviceInfoRepository;
        private IDeliveryLocationRepository _deliveryLocationRepository;
        private INotificationService _notificationService;

        public CheckoutService(ICategoryRepository categoryRepository, INotificationService notificationService, IDeliveryLocationRepository deliveryLocationRepository, IUserDeviceInfoRepository userDeviceInfoRepository, IDelivery_chargeRepository delivery_chargeRepository, ICartDetailsRepository cartDetailsRepository, IProductRepository productRepository, ISubcategoryRepository filterRepository, INotificationRepository notificationRepository, IBannerRepository bannerRepository, IVersionControlRepository versionControlRepository, IAppThemeRepository appThemeRepository, IEmailService emailService, IForgotPasswordRepository forgotPasswordRepository, IApplicationUserRepository applicationUserRepository, IOrderDetailsRepository orderDetailsRepository, IOrderRepository orderSummaryRepository, IUserAddressRepository userAddressRepository, IProductImagesRepository productImagesRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _productImagesRepository = productImagesRepository;
            _userAddressRepository = userAddressRepository;
            _orderRepository = orderSummaryRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _applicationUserRepository = applicationUserRepository;
            _forgotPasswordRepository = forgotPasswordRepository;
            _appThemeRepository = appThemeRepository;
            _emailService = emailService;
            _versionControlRepository = versionControlRepository;
            _bannerRepository = bannerRepository;
            _notificationRepository = notificationRepository;
            _filterRepository = filterRepository;
            _productRepository = productRepository;
            _cartDetailsRepository = cartDetailsRepository;
            _delivery_chargeRepository = delivery_chargeRepository;
            _userDeviceInfoRepository = userDeviceInfoRepository;
            _deliveryLocationRepository = deliveryLocationRepository;
            _notificationService = notificationService;
        }

        #region AddToCart
        /// <summary>
        /// This method is used to add product into the cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public AddToCartResponse AddToCart(AddToCart request)
        {

            AddToCartResponse res = new AddToCartResponse();
            var cart = _cartDetailsRepository.FindByApplicationUserId(request.ApplicationUserId);
            foreach (var record in cart)
            {
                if (record.ProductId == request.ProductId)
                {
                    res.error = true;
                    res.Message = StringConstants.ItemExist;
                    return res;
                }
            }
            try
            {
                CartDetails CartItem = new CartDetails()
                {
                    ApplicationUserId = request.ApplicationUserId,
                    ProductId = request.ProductId,
                    Quantity = 1,
                };
                if (request.ColorsId != 0)
                {
                    CartItem.ColorsId = request.ColorsId;
                }
                if (request.SizeId != 0)
                {
                    CartItem.SizeId = request.SizeId;
                }
                _cartDetailsRepository.Add(CartItem);
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.ItemAdded;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region IncDecProductQuantity
        /// <summary>
        /// This method is used to increment or decrement products quantity in cart
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IncDecProductResponse IncDecProductQuantity(IncDecProductResource request)
        {
            IncDecProductResponse res = new IncDecProductResponse();
            try
            {
                var pro = _cartDetailsRepository.FindByProductDetailId(request.ProductId, request.AppId, request.ApplicationUserId);
                var product = _productRepository.FindById(request.ProductId);
                if (product.IsAutomateStockMaintainance == true && request.Quantity > product.AvailableQuantity)
                {
                    res.error = true;
                    res.Message = StringConstants.CantAddMore;
                    return res;
                }
                pro.Quantity = request.Quantity;
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.QuantityUpdated;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetCartList
        /// <summary>
        /// This method is used to fetch cart list
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<GetCartListResponse> GetCartList(string AppId, int ApplicationUserId)
        {
            GetCartListResponse res = new GetCartListResponse();
            List<int> DeliveryDays = new List<int>();
            try
            {
                AddToCartResourceWrapper response = new AddToCartResourceWrapper
                {
                    CartList = new List<CartResponse>()
                };
                var CartRecordsList = await _cartDetailsRepository.GetAllItemsForUser(AppId, ApplicationUserId);
                response.CartList = CartRecordsList.Select(x => new CartResponse()
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList();
                foreach (var rec in response.CartList)
                {
                    var pro = await _productRepository.GetProductDetailAsync(rec.ProductId, AppId);
                    var record = _cartDetailsRepository.FindByCartDetailsId(rec.Id);
                    rec.ProductName = pro.Name + " (" + pro.Quantity + pro.Unit.UnitName + ")";
                    rec.ProductImageURL = EnvironmentConstants.KMMImageUrl + pro.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image;
                    rec.OriginalPrice = pro.OriginalPrice;
                    rec.ColorCode = record.Colors?.HashCode;
                    rec.Size = record.SIze?.ProductSize;
                    rec.DiscountedPrice = pro.DiscountedPrice;
                    rec.ShortDescription = pro.Description;
                    rec.AvailableQuantity = pro.AvailableQuantity;
                    DeliveryDays.Add(pro.DeliveryDays);

                }
                foreach (var record in response.CartList)
                {
                    var pro = _productRepository.FindById(record.ProductId);
                    if (pro.IsAutomateStockMaintainance == false)
                    {
                        record.IsAvailable = true;
                    }
                    else if (pro.IsAutomateStockMaintainance == true && pro.AvailableQuantity < record.Quantity)
                    {
                        record.IsAvailable = false;
                    }
                    else
                    {
                        record.IsAvailable = true;
                    }
                }
                response.TotalCount = CartRecordsList.Count;
                //var max = DeliveryDays.Max();

                res.Message = StringConstants.Success;
                res.data = response.CartList;
                res.Count = response.TotalCount;
                //res.EstimatedDeliveryDate = DateTime.Now.AddDays(max).ToString("dd-MM-yyyy");
                return res;


            }
            catch (Exception e)
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region AddNewAddress
        /// <summary>
        /// This method is used is used to add new address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CommonAddressResponse AddAddress(UsersAddress request)
        {
            CommonAddressResponse res = new CommonAddressResponse();
            try
            {
                UserAddress add = new UserAddress()
                {
                    ApplicationUserId = request.ApplicationUserId,
                    MobileNumber = request.MobileNumber,
                    Pincode = request.PINCode,
                    Address = request.Address,
                    Landmark = request.Landmark,
                    Locality = request.Locality,
                    DeliveryLocationId = request.PinCodeId == 0 ? null : request.PinCodeId
                };
                _userAddressRepository.Add(add);
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.AddressAdded;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }

        #endregion

        #region EditAddress
        /// <summary>
        /// This method is used to edit address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CommonAddressResponse EditAddress(UsersAddress request)
        {
            CommonAddressResponse res = new CommonAddressResponse();
            try
            {
                var exadd = _userAddressRepository.FindById(request.Id);
                exadd.MobileNumber = request.MobileNumber;
                exadd.Pincode = request.PINCode;
                exadd.Address = request.Address;
                exadd.Landmark = request.Landmark;
                exadd.Locality = request.Locality;
                exadd.DeliveryLocationId = request.PinCodeId == 0 ? null : request.PinCodeId;
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.AddressUpdated;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }

        #endregion

        #region DeleteAddress
        /// <summary>
        /// This method is used to delete address
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CommonAddressResponse DeleteAddress(DeleteAddressResource request)
        {
            CommonAddressResponse res = new CommonAddressResponse();
            try
            {
                var rec = _userAddressRepository.FindById(request.Id);
                rec.IsDeleted = true;
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.AddressDeleted;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetAddressList
        /// <summary>
        /// This method is used to fetch address list
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<AddressListResponse> GetAddressList(string AppId, int ApplicationUserId)
        {
            AddressListResponse res = new AddressListResponse();
            try
            {
                AddressResourceWrapper response = new AddressResourceWrapper
                {
                    AddressList = new List<UsersAddressResponse>()
                };

                var AddressRecordsList = await _userAddressRepository.GetAllAddressForUser(AppId, ApplicationUserId);

                response.AddressList = AddressRecordsList.Select(x => new UsersAddressResponse()
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    MobileNumber = x.MobileNumber,
                    PINCode = x.Pincode,
                    Address = x.Address,
                    Landmark = x.Landmark,
                    Locality = x.Locality,
                }).ToList();
                foreach (var rec in response.AddressList)
                {
                    var user = _applicationUserRepository.FindById(ApplicationUserId);
                    rec.Name = user.Name;
                }
                response.TotalCount = response.AddressList.Count;
                res.Message = StringConstants.Success;
                res.data = response.AddressList;
                res.Count = response.TotalCount;
                return res;

            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetPriceDetails
        /// <summary>
        /// This method is used to calculate price details
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<PriceDetailsResponse> GetPriceDetails(string AppId, int ApplicationUserId)
        {
            PriceDetailsResponse res = new PriceDetailsResponse();
            res =await PriceDetails(AppId, ApplicationUserId);
            return res;
        }
        #endregion

        #region PlaceOrder
        /// <summary>
        /// This is post method for place order
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PlaceOrderResponse> PlaceOrder(PlaceOrderResource request)
        {
            PriceDetailsResponse item = new PriceDetailsResponse();
            item = await PriceDetails(request.AppId, request.ApplicationUserId);
            PlaceOrderResponse res = new PlaceOrderResponse();
            try
            {
                if (request.SubTotal == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.SubtotalPriceZero;
                    return res;
                }
                PriceDetailsResponse rec = new PriceDetailsResponse();
                rec = await PriceDetails(request.AppId, request.ApplicationUserId);
                if(rec.error == false)
                {
                    if(rec.data.MRP != request.SubTotal || rec.data.SubTotal != request.MRP)
                    {
                        res.error = true;
                        res.Message = StringConstants.RateChanged;
                        return res;
                    }
                }
                foreach (var order in request.OrderDetails)
                {
                    var product = _productRepository.FindById(order.ProductDetailId);
                    if (product.IsAutomateStockMaintainance == true && order.Quantity > product.AvailableQuantity)
                    {
                        res.error = true;
                        res.Message = StringConstants.OutOfStock;
                        return res;
                    }
                }
                var address = _userAddressRepository.FindById(request.AddressId);
                //? DeliveryLocationId = FindDeliveryLocationId(request.AddressId);
                var estimatedDeliveryDate = CalculateDeliveryDate(DateTime.Now, address.DeliveryLocationId.Value);
                Order obj = new Order()
                {
                    MRP = request.MRP,
                    DiscountPrice = request.ProductsDiscount,
                    DeliveryCharges = request.DeliveryCharges,
                    SubTotalPrice = request.SubTotal,
                    UserAddressId = request.AddressId,
                    ApplicationUserId = request.ApplicationUserId,
                    CreatedDate = DateTime.Now,
                    Comments = request.Comments,
                    PaymentStatusId = 1,
                    DeliveryStatusId = 1,
                    EstimatedDeliveryDate = estimatedDeliveryDate,
                    TotalPrice = request.SubTotal - request.ProductsDiscount + request.DeliveryCharges
                };
                var OrderNumber = DateTime.Now.ToString("yyyyMMddHHmmssf");
                obj.OrderNumber = OrderNumber;

                _orderRepository.Add(obj);
                _unitOfWork.SaveChanges();

                foreach (var order in request.OrderDetails)
                {
                    var pro = await _productRepository.GetProductDetailAsync(order.ProductDetailId, request.AppId);
                    int discount = 0;
                    if (pro.OriginalPrice != 0)
                    {
                        discount = Convert.ToInt32(pro.OriginalPrice - pro.DiscountedPrice) * order.Quantity;
                    }
                    Entities.Database.OrderDetail ob = new Entities.Database.OrderDetail()
                    {
                        OrderId = obj.Id,
                        ProductId = order.ProductDetailId,
                        ColorsId = null,
                        SizeId = null,
                        Quantity = order.Quantity,
                        OriginalPrice = pro.OriginalPrice,
                        DiscountedPrice = pro.DiscountedPrice,
                    };
                    _orderDetailsRepository.Add(ob);
                    _unitOfWork.SaveChanges();

                    var cart = _cartDetailsRepository.FindByProductsDetailId(order.ProductDetailId, request.AppId, request.ApplicationUserId);
                    if (cart != null)
                    {
                        _cartDetailsRepository.Remove(cart);
                        _unitOfWork.SaveChanges();
                    }
                    if (pro.IsAutomateStockMaintainance == true)
                    {
                        pro.AvailableQuantity = pro.AvailableQuantity - order.Quantity;
                    }
                }

                res.Message = StringConstants.OrderPlaced;
                res.OrderId = obj.Id;
                res.OrderNumber = obj.OrderNumber;
                // var address = _userAddressRepository.FindById(request.AddressId);
                var deliveryLocation = _deliveryLocationRepository.findByLocalityAndPincode(address.Locality, address.Pincode);
                res.DeliveryDate = estimatedDeliveryDate.ToStringDatePattern() + ", " + deliveryLocation.Time;
                await SendReciptEmail(obj);

                AddNotificationViewModel model = new AddNotificationViewModel();
                model.Title = "Order Placed Successfully";
                model.Message = StringConstants.GetOrderNotificationMessage(obj.OrderNumber);
                model.Type = "text";
                int notificationId = await _notificationService.AddNotificationAsync(model, request.ApplicationUserId);

                return res;

            }
            catch (Exception e)
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                res.MessageError = e.Message;
                return res;
            }
        }


        #endregion

        #region GetMyOrders
        /// <summary>
        /// This method is used to fetch all orders
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<GetMyOrdersResponse> GetMyOrders(string AppId, int ApplicationUserId)
        {
            GetMyOrdersResponse res = new GetMyOrdersResponse();
            List<int> DeliveryDays = new List<int>();
            try
            {
                List<Order> list = await _orderRepository.GetAllOrders(ApplicationUserId);
                res.data = list.Select(x => new OrderResponse()
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    OrderedDate = x.CreatedDate.ToStringDatePattern(),
                    DeliveryStatus = x.DeliveryStatusId,
                    CreatedDate = x.CreatedDate,
                }).ToList();
                foreach (var order in res.data)
                {
                    var OrderInfo = _orderRepository.FindById(order.Id);
                    if (OrderInfo.DeliveredDate == null)
                    {
                        var address = _userAddressRepository.FindById(OrderInfo.UserAddressId);
                        var deliveryLocation = _deliveryLocationRepository.findByLocalityAndPincode(address.Locality, address.Pincode);
                        order.DeliveryDay = OrderInfo.EstimatedDeliveryDate.ToStringDatePattern() + ", " + deliveryLocation.Time;
                        if (OrderInfo.EstimatedDeliveryDate.Date < DateTime.Now.Date)
                        {
                            order.DeliveryDay = order.DeliveryDay + " (Delayed)";
                        }
                    }
                    if (OrderInfo.DeliveredDate != null)
                    {
                        order.DeliveredDate = OrderInfo.DeliveredDate.Value.ToStringDatePattern();
                    }
                }
                List<OrderResponse> DeliveryDateNotNull = res.data.Where(x => x.DeliveryDay != null && x.DeliveryStatus != 5).ToList();
                DeliveryDateNotNull = DeliveryDateNotNull.OrderBy(x => x.DeliveryDay).ToList();
                List<OrderResponse> DeliveredDateNotNull = res.data.Where(x => x.DeliveredDate != null || x.DeliveryStatus == 5).ToList();
                DeliveredDateNotNull = DeliveredDateNotNull.OrderByDescending(x => x.CreatedDate).ToList();

                List<OrderResponse> SortedList = DeliveryDateNotNull.Select(x => new OrderResponse()
                {
                    Id = x.Id,
                    OrderNumber = x.OrderNumber,
                    OrderedDate = x.OrderedDate,
                    DeliveryDay = x.DeliveryDay,
                    DeliveredDate = x.DeliveredDate,
                    DeliveryStatus = x.DeliveryStatus,
                }).ToList();

                foreach (var record in DeliveredDateNotNull)
                {
                    SortedList.Add(record);
                }

                res.data = SortedList;
                res.Count = res.data.Count;
                res.Message = StringConstants.Success;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetOrderDetails
        /// <summary>
        /// This method is used to fetch order details
        /// </summary>
        /// <param name="AppId"></param>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public async Task<OrderDetailsResponse> GetOrderDetails(string AppId, int OrderId)
        {
            OrderDetailsResponse res = new OrderDetailsResponse();
            try
            {
                var order = await _orderRepository.GetOrderDetails(OrderId);
                res.data = new OrderDetailsData();
                res.data.OrderDetails = new MyOrderDetails();
                res.data.OrderDetails.OrderedDate = order.CreatedDate.ToStringDatePattern();
                res.data.OrderDetails.OrderNumber = order.OrderNumber;
                res.data.OrderDetails.DeliveryStatus = order.DeliveryStatusId;
                if (order.DeliveredDate == null)
                {
                    var address = _userAddressRepository.FindById(order.UserAddressId);
                    var deliveryLocation = _deliveryLocationRepository.findByLocalityAndPincode(address.Locality, address.Pincode);
                    res.data.OrderDetails.DeliveryDay = order.EstimatedDeliveryDate.ToStringDatePattern() + ", " + deliveryLocation.Time;
                    if (order.EstimatedDeliveryDate.Date < DateTime.Now.Date)
                    {
                        res.data.OrderDetails.DeliveryDay = res.data.OrderDetails.DeliveryDay + " (Delayed)";
                    }
                }
                else
                {
                    res.data.OrderDetails.DeliveredDate = order.DeliveredDate.Value.ToStringDatePattern();
                }
                res.data.OrderDetails.OrderTotal = order.MRP;
                res.data.OrderDetails.Address = order.UserAddress.Address + ", " + order.UserAddress.Landmark + ", " + order.UserAddress.Locality + ", " + order.UserAddress.Pincode;

                var ProductsForOrderList = _orderDetailsRepository.GetAllItemsForOrder(OrderId);
                res.data.ProductList = ProductsForOrderList.Select(x => new Entities.ApiResponseResource.OrderDetailModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList();
                foreach (var rec in res.data.ProductList)
                {
                    var pro = await _productRepository.GetProductDetailAsync(rec.ProductId, StringConstants.AppId);
                    rec.ProductName = pro.Name + " (" + pro.Quantity + pro.Unit.UnitName + ")";
                    rec.ProductImageURL = EnvironmentConstants.KMMImageUrl + pro.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image;
                    if (pro.OriginalPrice == null)
                    {
                        rec.OriginalPrice = 0;
                    }
                    else
                    {
                        rec.OriginalPrice = pro.OriginalPrice;
                    }
                    rec.DiscountedPrice = pro.DiscountedPrice;
                    rec.ShortDescription = pro.Description;

                    var OrderDetails = _orderDetailsRepository.GetProductForOrder(OrderId, rec.ProductId);
                    rec.ColorCode = OrderDetails.Colors?.HashCode;
                    rec.Size = OrderDetails.Size?.ProductSize;
                }
                res.ProductsCount = res.data.ProductList.Count;


                res.Message = StringConstants.Success;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region UpdateTransactionId
        /// <summary>
        /// This method is used to update transaction ID
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="TransactionId"></param>
        /// <returns></returns>
        public async Task<CommonResponse> UpdateTransactionId(int OrderId, string TransactionId)
        {
            CommonResponse res = new CommonResponse();
            try
            {
                var order = await _orderRepository.FindByIdAsync(OrderId);
                order.TransactionId = TransactionId;
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.TransactionIdUpdated;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region AddToken
        /// <summary>
        /// This method is used to add token
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public CommonResponse AddToken(string Token, int? ApplicationUserId)
        {
            CommonResponse res = new CommonResponse();
            try
            {
                var record = _userDeviceInfoRepository.FindByToken(Token, ApplicationUserId);
                if (record != null)
                {
                    res.error = true;
                    res.Message = StringConstants.TokenExist;
                    return res;
                }
                if (ApplicationUserId == 0)
                {
                    UserDeviceInfo Info = new UserDeviceInfo();
                    Info.DeviceId = Token;
                    _userDeviceInfoRepository.Add(Info);
                    _unitOfWork.SaveChanges();
                    res.Message = StringConstants.TokenAdded;
                    return res;
                }
                else
                {
                    UserDeviceInfo Info = new UserDeviceInfo();
                    Info.DeviceId = Token;
                    Info.ApplicationUserId = ApplicationUserId;
                    _userDeviceInfoRepository.Add(Info);
                    _unitOfWork.SaveChanges();
                    res.Message = StringConstants.TokenAdded;
                    return res;
                }
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetProductsForOrder
        public async Task<GetProductForOrderResponse> GetProductsForOrder(int OrderId)
        {
            GetProductForOrderResponse res = new GetProductForOrderResponse();
            try
            {
                var ProductsForOrderList = _orderDetailsRepository.GetAllItemsForOrder(OrderId);
                res.data = ProductsForOrderList.Select(x => new Entities.ApiResponseResource.OrderDetailModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList();
                foreach (var rec in res.data)
                {
                    var pro = await _productRepository.GetProductDetailAsync(rec.ProductId, StringConstants.AppId);
                    rec.ProductName = pro.Name;
                    rec.ProductImageURL = EnvironmentConstants.KMMImageUrl + pro.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image;
                    rec.OriginalPrice = pro.OriginalPrice;
                    rec.DiscountedPrice = pro.DiscountedPrice;
                    rec.ShortDescription = pro.Description;
                }
                res.Count = res.data.Count;

                res.Message = StringConstants.Success;
                return res;


            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetDeliveryDay
        /// <summary>
        /// This method is used to calculate delivery day
        /// </summary>
        /// <param name="AddressId"></param>
        /// <returns></returns>
        public DeliveryDayResponseResource GetDeliveryDay(int AddressId)
        {
            DeliveryDayResponseResource res = new DeliveryDayResponseResource();
            try
            {
                var address = _userAddressRepository.FindById(AddressId);
                var deliveryLocation = _deliveryLocationRepository.findByLocalityAndPincode(address.Locality, address.Pincode);
                var deliveryDate = CalculateDeliveryDate(DateTime.Now, address.DeliveryLocationId.Value);
                res.Message = StringConstants.Success;
                res.DeliveryDay = deliveryDate.ToStringDatePattern() + ", " + deliveryLocation.Time;
                return res;
            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region RemoveFromCart
        /// <summary>
        /// This method is used to remove product from cart
        /// </summary>
        /// <param name="ApplicationUserId"></param>
        /// <param name="ProductId"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public CommonResponse RemoveFromCart(int ApplicationUserId, int ProductId, string AppId)
        {
            CommonResponse res = new CommonResponse();
            try
            {
                var rec = _cartDetailsRepository.FindByProductDetailId(ProductId, AppId, ApplicationUserId);
                _cartDetailsRepository.Remove(rec);
                _unitOfWork.SaveChanges();
                res.Message = StringConstants.ProductRemovedFromCart;
                return res;
            }
            catch (Exception e)
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region get deliver chart
        /// <summary>
        /// This method is used to fetch delivery chart
        /// </summary>
        /// <returns></returns>
        public async Task<DeliveryLocationListResponseWrapper> GetDeliveryLocations()
        {
            DeliveryLocationListResponseWrapper response = new DeliveryLocationListResponseWrapper();
            try
            {
                var deliveryLocations = await _deliveryLocationRepository.GetAllAsync();
                response.data = deliveryLocations.Select(x => new DeliveryLocationListResponse
                {
                    Id = x.Id,
                    Area = x.Area,
                    Time = x.Time,
                    Day = x.Day,
                    PinCode = x.Pincode
                }).ToList();
                return response;
            }
            catch (Exception e)
            {
                response.error = true;
                response.Message = StringConstants.ServerError;
                return response;
            }
        }
        #endregion


        #region Private
        #region Send Email

        private Task SendReciptEmail(Order order)
        {

            var userInfo = _applicationUserRepository.FindById(order.ApplicationUserId);
            MailReciptViewModel model = new MailReciptViewModel();
            model.CustomerComment = string.IsNullOrEmpty(order.Comments) ? null : order.Comments;
            model.CustomerDetails.Address.AreaPincode = order.UserAddress?.Locality + " " + order.UserAddress?.Pincode;
            model.CustomerDetails.Address.Address = order.UserAddress?.Address;
            model.CustomerDetails.Address.Landmark = order.UserAddress?.Landmark;
            model.CustomerDetails.Address.PhoneNumber = order.UserAddress?.MobileNumber;

            model.CustomerDetails.Email = userInfo.Email;
            model.CustomerDetails.MobileNo = userInfo.MobileNumber;
            model.CustomerDetails.Name = userInfo.Name;

            model.FinalAmountDetails.DeliveryCharges = order.DeliveryCharges == 0 ? "Free" : order.DeliveryCharges.ToString();
            model.FinalAmountDetails.ProductDiscount = Math.Round(order.DiscountPrice, 2).ToString();
            model.FinalAmountDetails.SubTotal = Math.Round(order.SubTotalPrice, 2).ToString();
            model.FinalAmountDetails.TotalPrice = order.MRP.ToString();

            model.OrderDateTime = order.CreatedDate.ToStringDayOfWeekPattern();
            model.OrderNumber = order.OrderNumber;
            model.ProductList = order.OrderDetails.Select(x => new OrderProductDetails()
            {
                Price = Math.Round(x.DiscountedPrice, 2).ToString(),
                Quantity = x.Quantity.ToString(),
                Product = x.Product.Name,
                Id = x.ProductId
            }).ToList();
            foreach (var pro in model.ProductList)
            {
                var product = _productRepository.FindById(pro.Id);
                pro.Product = product.Name + " (" + product.Quantity + product.Unit.UnitName + ")";
            }

            return _emailService.SendEmail(userInfo.Email, _emailService.OrderReciptTemplateBody(model), "Kerala Mini Mart - Recipt", true);
        }
        #endregion

        #region Calculate deliver day
        private DateTime CalculateDeliveryDate(DateTime orderedDate, int deliveryLocationId)
        {
            var deliveryDatesInNext7Days = GetThisWeekDeliveryDates(orderedDate, deliveryLocationId);
            var nextWeekLatestDeliveryDate = deliveryDatesInNext7Days.Min().AddDays(7);
            bool IsGotDate = false;
            DateTime dateTime = nextWeekLatestDeliveryDate;
            foreach (var day in deliveryDatesInNext7Days)
            {
                if (IsGotDate == true)
                {
                    dateTime = day;
                    break;
                }
                else if (orderedDate == day)
                {
                    IsGotDate = true;
                    continue;
                }
                else if (orderedDate < day)
                {
                    dateTime = day;
                    break;
                }
            }
            return dateTime;
        }

        private List<DateTime> GetThisWeekDeliveryDates(DateTime orderBookingDate, int deliveryLocationId)
        {
            var deliveryLocationDays = _deliveryLocationRepository.GetSimilaryDeliveryDaysAtLocation(deliveryLocationId);
            var next7DatesList = Enumerable.Range(0, 7).Select(i => orderBookingDate.AddDays(i)).ToList();
            List<DateTime> days = new List<DateTime>();
            foreach (var record in deliveryLocationDays)
            {
                var day = next7DatesList.Where(x => x.DayOfWeek.ToString() == record.Day).FirstOrDefault();
                if (day == null)
                    continue;
                days.Add(day);
            }
            return days.OrderBy(x => x).ToList();
        }
        #endregion

        //#region FindDeliveryLocationId
        //private int FindDeliveryLocationId(int AddressId)
        //{
        //    var record = _userAddressRepository.FindById(AddressId);
        //    return (record.DeliveryLocationId);
        //}



        //#endregion

        #region PriceDetails
        private async Task<PriceDetailsResponse> PriceDetails(string AppId, int ApplicationUserId)
        {
            PriceDetailsResponse res = new PriceDetailsResponse();
            try
            {
                AddToCartResourceWrapper response = new AddToCartResourceWrapper
                {
                    CartList = new List<CartResponse>()
                };
                var CartRecordsList = await _cartDetailsRepository.GetAllItemsForUser(AppId, ApplicationUserId);
                response.CartList = CartRecordsList.Select(x => new CartResponse()
                {
                    Id = x.Id,
                    ApplicationUserId = x.ApplicationUserId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                }).ToList();
                foreach (var rec in response.CartList)
                {
                    var pro = await _productRepository.GetProductDetailAsync(rec.ProductId, AppId);
                    rec.ProductName = pro.Name;
                    rec.OriginalPrice = pro.OriginalPrice;
                    rec.DiscountedPrice = pro.DiscountedPrice;
                }
                response.TotalCount = CartRecordsList.Count;
                PriceDetailsResource obj = new PriceDetailsResource();
                foreach (var item in response.CartList)
                {
                    if (item.OriginalPrice != null)
                    {
                        obj.MRP = Convert.ToInt32(obj.MRP + (item.OriginalPrice * item.Quantity));
                    }
                    else
                    {
                        obj.MRP = Convert.ToInt32(obj.MRP + (item.DiscountedPrice * item.Quantity));
                    }
                    int discount = 0;
                    if (item.OriginalPrice != null)
                    {
                        discount = (Convert.ToInt32(item.OriginalPrice - item.DiscountedPrice)) * item.Quantity;
                    }
                    obj.ProductDiscounts = obj.ProductDiscounts + discount;
                }
                int total = obj.MRP - obj.ProductDiscounts;
                if (total >= 251)
                {
                    var rec = _delivery_chargeRepository.findByMin(251, AppId);
                    obj.DeliveryCharges = rec.Charge;
                }

                else if (total >= 1 && total <= 250)
                {
                    var rec = _delivery_chargeRepository.findByMin(1, AppId);
                    obj.DeliveryCharges = rec.Charge;
                }
                obj.SubTotal = total + obj.DeliveryCharges;
                res.Message = StringConstants.Success;
                res.data = obj;
                return res;

            }
            catch
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #endregion
    }
}
