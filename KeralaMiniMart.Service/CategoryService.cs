using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.ApiResponseResource;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Service.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Service
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private IEmailService _emailService;
        private IProductImagesRepository _productImagesRepository;
        private IUserAddressRepository _userAddressRepository;
        private IOrderRepository _orderSummaryRepository;
        private IOrderDetailsRepository _orderDetailsRepository;
        private IApplicationUserRepository _applicationUserRepository;
        private IForgotPasswordRepository _forgotPasswordRepository;
        private IAppThemeRepository _appThemeRepository;
        private IVersionControlRepository _versionControlRepository;
        private IBannerRepository _bannerRepository;
        private INotificationRepository _notificationRepository;
        private ISubcategoryRepository _filterRepository;
        private IProductRepository _productRepository;


        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, ISubcategoryRepository filterRepository, INotificationRepository notificationRepository, IBannerRepository bannerRepository, IVersionControlRepository versionControlRepository, IAppThemeRepository appThemeRepository, IEmailService emailService, IForgotPasswordRepository forgotPasswordRepository, IApplicationUserRepository applicationUserRepository, IOrderDetailsRepository orderDetailsRepository, IOrderRepository orderSummaryRepository, IUserAddressRepository userAddressRepository, IProductImagesRepository productImagesRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _productImagesRepository = productImagesRepository;
            _userAddressRepository = userAddressRepository;
            _orderSummaryRepository = orderSummaryRepository;
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
        }

        public CategoryService()
        {

        }

        #region GetAppTheme
        /// <summary>
        /// This method is used to fetch App Theme
        /// </summary>
        /// <returns></returns>
        public async Task<ApplicationThemeResponse> GetAppTheme()
        {
            ApplicationThemeResponse res = new ApplicationThemeResponse();
            var appConfigs = await _appThemeRepository.GetAllAsync();
            var appConfig = appConfigs.LastOrDefault<AppTheme>();
            var appTheme = new AppThemeResponse()
            {
                PrimaryColor = appConfig.PrimaryColor,
                SecondryColor = appConfig.SecondryColor,
                StatusBarColor = appConfig.StatusBarColor,
                TertiaryColor = appConfig.TertiaryColor,
                TextColor = appConfig.PrimaryTextColor,
                AppName = appConfig.AppName,
                AppLogoURL = EnvironmentConstants.KMMImageUrl + appConfig.AppLogo,
                CurrencySymbol = appConfig.CurrencySymbols

            };
            res.Message = StringConstants.Success;
            res.data = appTheme;
            return res;
        }
        #endregion


        #region VersionInfo
        /// <summary>
        /// This method is used to fetch version info
        /// </summary>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public VersionResponse GetVersion(string AppId)
        {
            VersionResponse res = new VersionResponse();
            try
            {
                var model = _versionControlRepository.CurrentVersion(AppId);
                res.Message = StringConstants.Message;
                res.data = model;
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


        #region GetAllBanner
        /// <summary>
        /// This method is used to fetch banner list
        /// </summary>
        /// <param name="take"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public async Task<BannerResponse> GetBanner(int take, string AppId)
        {
            BannerResponse res = new BannerResponse();
            try
            {
                var data = await _bannerRepository.GetBannerList(0, take, AppId);
                BannerResourceWrapper response = new BannerResourceWrapper
                {
                    BannerList = new List<BannerResource>()
                };
                response.BannerList = data.Select(x => new BannerResource()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Image = EnvironmentConstants.KMMImageUrl + x.Image

                }).ToList();
                res.Message = StringConstants.Message;
                res.data = response;
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


        #region GetAllCategory
        /// <summary>
        /// This method is used to fetch category list
        /// </summary>
        /// <param name="take"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public async Task<CategoryResponse> GetCategory(int take, string AppId)
        {
            CategoryResponse res = new CategoryResponse();
            try
            {
                var data = await _categoryRepository.GetCategoryList(0, take, AppId);
                CategoryResourceWrapper response = new CategoryResourceWrapper
                {
                    CategoryList = new List<CategoryResource>()
                };
                response.CategoryList = data.Select(x => new CategoryResource()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = EnvironmentConstants.KMMImageUrl + x.Image

                }).ToList();
                res.Message = StringConstants.Message;
                res.data = response;
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


        #region GetAllNotifications
        /// <summary>
        /// This method is used to fetch notification list
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="AppId"></param>
        /// <param name="ApplicationUserId"></param>
        /// <returns></returns>
        public async Task<NotificationResponse> GetNotification(int PageIndex,int PageSize, string AppId, int ApplicationUserId)
        {
            NotificationResponse res = new NotificationResponse();
            try
            {
                List < Notification > data= new List<Notification>();
                if (ApplicationUserId == 0)
                {
                     data = await _notificationRepository.GetNotificationList(PageSize * (PageIndex - 1), PageSize, AppId);
                }
                else
                {
                     data = await _notificationRepository.GetNotificationListForUser(PageSize * (PageIndex - 1), PageSize, AppId, ApplicationUserId);
                }
                NotificationResourceWrapper response = new NotificationResourceWrapper
                {
                    NotificationList = new List<NotificationResource>()
                };
                response.NotificationList = data.Select(x => new NotificationResource()
                {
                    Id = x.Id,
                    NotificationType = x.NotificationType,
                    TimeStamp = x.CreatedDateTime.ToTimeStamp().ToString(),
                    ImageUrl = EnvironmentConstants.KMMImageUrl+ x.ImageUrl,
                    Message = x.Message,
                    Title = x.Title,
                    ModifiedTitle = x.CategoryId == null ? x.Title : x.Title + " (click here)",
                    CategoryId = x.CategoryId == null ? 0 : x.CategoryId.Value,
                    Category = x.CategoryId == null ? null : x.Category.Name
                }).ToList();
                res.Message = StringConstants.Message;
                res.data = response;
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


        #region GetAllFilters
        /// <summary>
        /// This method is used to fetch filters list
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public async Task<FilterResponse> GetFilter(int CategoryId, string AppId)
        {
            FilterResponse res = new FilterResponse();
            try
            {
                if (CategoryId == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.CatNotFound;
                    return res;
                }
                var data = await _filterRepository.GetSubcategoryAsync(CategoryId, AppId);
                FilterResourceWrapper response = new FilterResourceWrapper
                {
                    FilterList = new List<FilterResource>()
                };
                response.FilterList = data.Select(x => new FilterResource()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
                res.Message = StringConstants.Message;
                res.data = response;
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


        #region GetProductListForSearch
        /// <summary>
        /// This method is used to fetch product list for searched items
        /// </summary>
        /// <param name="take"></param>
        /// <param name="AppId"></param>
        /// <param name="Search"></param>
        /// <returns></returns>
        public async Task<ProductResponse> GetProductListForSearch(int take, string AppId, string Search)
        {
            ProductResponse res = new ProductResponse();
            try
            {
                var data = await _productRepository.GetProductListForSearch(take, AppId, Search);
                ProductResourceWrapper response = new ProductResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };
                response.ProductList = data.Select(x => new ProductResource()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                    OriginalPrice = x.OriginalPrice,
                    DiscountedPrice = x.DiscountedPrice,
                    DiscountPercentage = x.DiscountPercentage,
                    CategoryName = x.Category.Name,
                    Quantity = x.Quantity,
                    Unit = x.Unit.UnitName,
                    IsAvailable = x.IsAutomateStockMaintainance,
                    AvailableQuantity = x.AvailableQuantity,
                    Filters = x.Category.Filters.Select(y => y.Name).ToList(),

                }).ToList();
                foreach (var record in response.ProductList)
                {
                    var pro = _productRepository.FindById(record.Id);
                    if (pro.IsAutomateStockMaintainance == false)
                    {
                        record.IsAvailable = true;
                    }
                    else if (pro.IsAutomateStockMaintainance == true && pro.AvailableQuantity == 0)
                    {
                        record.IsAvailable = false;
                    }
                }
                res.Message = StringConstants.Message;
                res.data = response;
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


        #region GetProductDetail
        /// <summary>
        /// This method is used to fetch product details
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="AppId"></param>
        /// <returns></returns>
        public ProductDetailsResponse GetProductDetail(int Id, string AppId)
        {
            ProductDetailsResponse res = new ProductDetailsResponse();
            try
            {
                var product = _productRepository.FindById(Id, AppId);
                if (product == null)
                {
                    res.error = true;
                    res.Message = StringConstants.ProductNotFound;
                    return res;
                }

                ProductDetailsResource pro = new ProductDetailsResource()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    OriginalPrice = product.OriginalPrice,
                    DiscountedPrice = product.DiscountedPrice,
                    DiscountPercent = product.DiscountPercentage,
                    Weight = product.Weight,
                    Height = product.Height,
                    Width = product.Width,
                    Length = product.Length,
                    Brand = product.Brand,
                    Quantity = product.Quantity,
                    UnitName = product.Unit?.UnitName,
                    MaterialType = product.MaterialType,
                    IncludedAccesories = product.IncludedAccessories,
                    Precautions = product.Precautions,
                    DeliveryTime ="Delivery in "+ product.DeliveryDays + " Days",

                    Images = product.ProductImages.Where(y => y.ProductId == product.Id).Select(y => new ProductImagesResource()
                    {
                        Image = EnvironmentConstants.KMMImageUrl + y.Image,
                    }).ToList(),
                    ColorList = product.ProductColors.Where(y => y.ProductId == product.Id).Select(y => new ProductColorsResource()
                    {
                        Id = y.Colors.Id,
                        ColorName = y.Colors.Name,
                        HashCode = y.Colors.HashCode,
                    }).ToList(),
                    SizeList = product.ProductSizes.Where(y => y.ProductId == product.Id).Select(y => new ProductSizeResource()
                    {
                        Id = y.Size.Id,
                        Name = y.Size.ProductSize,
                    }).ToList(),
                };
                if(product.IsAutomateStockMaintainance == false)
                {
                    pro.IsAvailable = true;
                }
                else if(product.IsAutomateStockMaintainance == true && product.AvailableQuantity == 0)
                {
                    pro.IsAvailable = false;
                }
                else
                {
                    pro.IsAvailable = true;
                }
                res.Message = StringConstants.Message;
                res.data = pro;
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

        #region GetAllProductForCategory
        /// <summary>
        /// This method is used to fetch all products for specific category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="AppId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public ProductListResponse GetAllProductForCategory(int CategoryId,  string AppId, int skip, int take)
        {
            ProductListResponse res = new ProductListResponse();
            try
            {
                ProductDetailsResourceWrapper response = new ProductDetailsResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };
                if (CategoryId == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.CatNotFound;
                    return res;
                }

                else
                {

                    List<Product> ProductRecordsList = _productRepository.GetAllProductForCategory(CategoryId, AppId, skip, take);
                    
                    var ProductRecordsForCount = _productRepository.GetAllProductForCategory(CategoryId, AppId);

                    response.ProductList = ProductRecordsList.Select(x => new ProductResource()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                        OriginalPrice = x.OriginalPrice,
                        DiscountedPrice = x.DiscountedPrice,
                        DiscountPercentage = x.DiscountPercentage,
                        IsExclusive = x.IsExclusive,
                        Quantity = x.Quantity,
                        Unit = x.Unit?.UnitName,
                        IsAvailable = x.IsAutomateStockMaintainance,
                        AvailableQuantity = x.AvailableQuantity,

                    }).ToList();
                    foreach (var record in response.ProductList)
                    {
                        var pro = _productRepository.FindById(record.Id);
                        if(pro.IsAutomateStockMaintainance == false)
                        {
                            record.IsAvailable = true;
                        }
                        else if (pro.IsAutomateStockMaintainance == true && pro.AvailableQuantity == 0)
                        {
                            record.IsAvailable = false;
                        }
                    }
                    response.TotalCount = ProductRecordsForCount.Count;
                }
                res.Message = StringConstants.Message;
                res.data = response;
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

        #region GetAllExclusiveProductForCategory
        /// <summary>
        /// This method is used to fetch all exclusive products for specific category
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="AppId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public ProductListResponse GetAllExclusiveProductForCategory(int CategoryId, string AppId, int skip, int take)
        {
            ProductListResponse res = new ProductListResponse();
            try
            {
                ProductDetailsResourceWrapper response = new ProductDetailsResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };
                if (CategoryId == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.CatNotFound;
                    return res;
                }

                else
                {

                    List<Product> ProductRecordsList = _productRepository.GetAllExclusiveProductForCategory(CategoryId, AppId, skip, take);

                    var ProductRecordsForCount = _productRepository.GetAllExclusiveProductForCategory(CategoryId, AppId);

                    response.ProductList = ProductRecordsList.Select(x => new ProductResource()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                        OriginalPrice = x.OriginalPrice,
                        DiscountedPrice = x.DiscountedPrice,
                        DiscountPercentage = x.DiscountPercentage,
                        IsExclusive = x.IsExclusive,
                        Quantity = x.Quantity,
                        Unit = x.Unit.UnitName,

                    }).ToList();
                    response.TotalCount = ProductRecordsForCount.Count;
                }
                res.Message = StringConstants.Message;
                res.data = response;
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

        #region ApplyAllFilters
        /// <summary>
        /// This method is used to fetch product list according to applied filters
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProductListResponse ApplyAllFilters(ApplyFiltersResource request)
        {
            ProductListResponse res = new ProductListResponse();
            try
            {
                ProductDetailsResourceWrapper response = new ProductDetailsResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };
                if (request.CategoryId == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.CatNotFound;
                    return res;
                }

                else
                {
                    response.FiltersList = new List<string>();
                    List<Product> ProductRecordsList = _productRepository.GetAllProductForCategory(request.CategoryId, request.AppId);

                    List<ProductResource> AllProductList = ProductRecordsList.Select(x => new ProductResource()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        FilterId = x.SubcategoryId,
                        Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                        OriginalPrice = x.OriginalPrice,
                        DiscountedPrice = x.DiscountedPrice,
                        DiscountPercentage = x.DiscountPercentage,
                        IsExclusive = x.IsExclusive,
                        AvailableQuantity = x.AvailableQuantity

                    }).ToList();
                    List<ProductResource> FilteredProductList = new List<ProductResource>();
                    foreach (var pro in AllProductList)
                    {
                        foreach (var filter in request.Filters)
                        {
                            if (pro.FilterId == filter)
                                FilteredProductList.Add(pro);
                            
                        }
                    }
                    foreach (var pro in FilteredProductList)
                    {
                        var product = _productRepository.FindProductById(pro.Id);
                        pro.Quantity = product.Quantity;
                        pro.Unit = product.Unit.UnitName;
                        if(product.IsAutomateStockMaintainance == false)
                        {
                            pro.IsAvailable = true;
                        }
                        if(product.IsAutomateStockMaintainance == true && product.AvailableQuantity == 0)
                        {
                            pro.IsAvailable = false;
                        }
                        else
                        {
                            pro.IsAvailable = true;
                        }
                    }
                    response.TotalCount = FilteredProductList.Count;
                    response.ProductList = FilteredProductList.Skip(request.skip).Take(request.take).ToList();
                }
                List<Subcategory> filters = new List<Subcategory>();
                filters = _filterRepository.GetByIds(request.Filters);
                response.FiltersList = filters.Select(x => x.Name).ToList();
                res.Message = StringConstants.Message;
                res.data = response;
                return res;
            }
            catch(Exception e)
            {
                res.error = true;
                res.Message = StringConstants.ServerError;
                return res;
            }
        }
        #endregion

        #region GetRecommendedProduct
        /// <summary>
        /// This method is used to fetch recommended products for specific product
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <param name="SubCategoryId"></param>
        /// <param name="ProductId"></param>
        /// <param name="AppId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public ProductListResponse GetRecommendedProduct(int CategoryId, int SubCategoryId, int ProductId, string AppId, int skip, int take)
        {
            Product product = _productRepository.FindById(ProductId);
            ProductListResponse res = new ProductListResponse();
            try
            {
                ProductDetailsResourceWrapper response = new ProductDetailsResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };
                if (CategoryId == 0)
                {
                    res.error = true;
                    res.Message = StringConstants.CatNotFound;
                    return res;
                }

                else if (product.SubcategoryId != null)
                {
                    var ProductRecordsList = _productRepository.GetAllProductForRecommendedSubCategory(product.SubcategoryId.Value, ProductId, AppId, skip, take);

                    var ProductRecordsForCount = _productRepository.GetAllProductForRecommendedSubCategory(product.SubcategoryId.Value, ProductId, AppId);
                    response.ProductList = ProductRecordsList.Select(x => new ProductResource()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                        OriginalPrice = x.OriginalPrice,
                        DiscountedPrice = x.DiscountedPrice,
                        DiscountPercentage = x.DiscountPercentage,
                        IsExclusive = x.IsExclusive,
                        IsAvailable = x.IsAutomateStockMaintainance,
                        Quantity = x.Quantity,
                        Unit = x.Unit.UnitName,

                    }).ToList();
                    foreach (var record in response.ProductList)
                    {
                        var pro = _productRepository.FindById(record.Id);
                        if (pro.IsAutomateStockMaintainance == false)
                        {
                            record.IsAvailable = true;
                        }
                        else if (pro.IsAutomateStockMaintainance == true && pro.AvailableQuantity == 0)
                        {
                            record.IsAvailable = false;
                        }
                    }
                    if (ProductRecordsForCount.Count != 0)
                    {
                        response.TotalCount = ProductRecordsForCount.Count;
                        res.Message = StringConstants.Message;
                        res.data = response;
                        return res;
                    }
                    else
                    {
                        res = GetRecommendedProduct(CategoryId, ProductId, AppId, skip, take);
                        return res;
                    }
                }

                else
                {

                    res = GetRecommendedProduct(CategoryId, ProductId, AppId, skip, take);
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

        #region Private Methods

        #region GetRecommendedProduct
        private ProductListResponse GetRecommendedProduct(int CategoryId,  int ProductId, string AppId, int skip, int take)
        {
            ProductListResponse res = new ProductListResponse();
            try
            {
                ProductDetailsResourceWrapper response = new ProductDetailsResourceWrapper
                {
                    ProductList = new List<ProductResource>()
                };

                var ProductRecordsList = _productRepository.GetAllProductForRecommended(CategoryId, ProductId, AppId, skip, take);

                var ProductRecordsForCount = _productRepository.GetAllProductForRecommended(CategoryId, ProductId, AppId);

                response.ProductList = ProductRecordsList.Select(x => new ProductResource()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = EnvironmentConstants.KMMImageUrl + x.ProductImages.Where(y => y.IsMain == true).FirstOrDefault().Image,
                    OriginalPrice = x.OriginalPrice,
                    DiscountedPrice = x.DiscountedPrice,
                    DiscountPercentage = x.DiscountPercentage,
                    Quantity = x.Quantity,
                    Unit = x.Unit.UnitName,
                    IsAvailable = x.IsAutomateStockMaintainance,
                    AvailableQuantity = x.AvailableQuantity,
                    IsExclusive = x.IsExclusive,

                }).ToList();
                foreach (var record in response.ProductList)
                {
                    var pro = _productRepository.FindById(record.Id);
                    if (pro.IsAutomateStockMaintainance == false)
                    {
                        record.IsAvailable = true;
                    }
                    else if (pro.IsAutomateStockMaintainance == true && pro.AvailableQuantity == 0)
                    {
                        record.IsAvailable = false;
                    }
                }
                response.TotalCount = ProductRecordsForCount.Count;
                res.Message = StringConstants.Message;
                res.data = response;
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
