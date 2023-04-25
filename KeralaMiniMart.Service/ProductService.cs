
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
using KeralaMiniMart.Service.ExtensionMethods;

namespace KeralaMiniMart.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _filterRepository;
        private readonly IColorsRepository _colorsRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly IProductImagesRepository _productImagesRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IUnitRepository _unitRepository;

        public ProductService(IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            ICategoryRepository categoryRepository,
            ISubcategoryRepository filterRepository,
            IColorsRepository colorsRepository,
            ISizeRepository sizeRepository,
            IProductColorRepository productColorRepository,
            IProductImagesRepository productImagesRepository,
            IProductSizeRepository productSizeRepository,
            IUnitRepository unitRepository
            )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
            _filterRepository = filterRepository;
            _colorsRepository = colorsRepository;
            _sizeRepository = sizeRepository;
            _productColorRepository = productColorRepository;
            _productImagesRepository = productImagesRepository;
            _productSizeRepository = productSizeRepository;
            _unitRepository = unitRepository;
        }

        #region AddProduct
        public async Task<int> AddProductAsync(AddProductViewModel model)
        {
            var databaseModel = new Product()
            {
                CategoryId = model.CategoryId,
                CreatedDateTime = DateTime.Now,
                ModifiedDateTime = DateTime.Now,
                DeliveryDays = model.DeliveryDays,
                Description = model.LongDescription,
                DiscountedPrice = model.DiscountedPrice,
                SubcategoryId = model.FilterId,
                IncludedAccessories = model.Accessories,
                IsAutomateStockMaintainance = model.IsAvailable,
                MaterialType = model.MaterialType,
                IsDeleted = false,
                IsExclusive = model.IsExclusive,
                IsPublish = model.Status.ToLower() == "publish",
                Name = model.ProductName,
                OriginalPrice = model.OriginalPrice,
                Precautions = model.PrecautionsInstructions,
                StockKeepingUnit = model.StockKeepingUnit,
                Weight = model.ProductWeight,
                Width = model.ProductWidth,
                Length = model.ProductLength,
                Height = model.ProductHeight,
                Brand = model.Brand,
                Quantity = model.Quantity,
                UnitId = model.UnitId,
                DiscountPercentage = 0,                    // here we have to calculate percentage
            };
            if (model.IsAvailable == true)
            {
                if (model.AvailableQuantity != null)
                {
                    databaseModel.AvailableQuantity = model.AvailableQuantity.Value;
                }
                else
                {
                    databaseModel.AvailableQuantity = 0;
                }
            }

            _productRepository.Add(databaseModel);
            await _unitOfWork.SaveChangesAsync();

            // save images
            await SaveImages(model.MainImageText, databaseModel.Id, true);
            await SaveImages(model.Image1RelativePath, databaseModel.Id, false);
            await SaveImages(model.Image2RelativePath, databaseModel.Id, false);
            await SaveImages(model.Image3RelativePath, databaseModel.Id, false);
            await SaveImages(model.Image4RelativePath, databaseModel.Id, false);
            // save colors
            await SaveColors(model.SelectedColorIds, databaseModel.Id);
            //save sizes
            await SaveSizes(model.SelectedSizeIds, databaseModel.Id);
            return databaseModel.Id;
        }
        #endregion

        #region Private Methods
        private async Task SaveSizes(List<int> selectedSizeIds, int productId)
        {
            foreach (var sizeId in selectedSizeIds)
            {
                ProductSize size = new ProductSize()
                {
                    SizeId = sizeId,
                    ProductId = productId
                };
                _productSizeRepository.Add(size);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task SaveColors(List<int> selectedColorIds, int productId)
        {
            foreach (var colorId in selectedColorIds)
            {
                ProductColor color = new ProductColor()
                {
                    ColorsId = colorId,
                    ProductId = productId
                };
                _productColorRepository.Add(color);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task SaveImages(string imageUrl, int productId, bool isMain)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ProductImages img = new ProductImages()
                {
                    Image = imageUrl,
                    IsMain = isMain,
                    ProductId = productId
                };
                _productImagesRepository.Add(img);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task UpdateMainImage(string imageUrl, int productId, bool isMain)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                ProductImages img = _productImagesRepository.FindMainImage(productId);
                img.Image = imageUrl;
                await _unitOfWork.SaveChangesAsync();
            }
        }
        #endregion

        #region GetAvailableSizeList
        public async Task<List<IdNameViewModel>> GetAvailableSizeList()
        {
            var list = await _sizeRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.ProductSize }).ToList();
            return responseList;
        }
        #endregion

        #region GetCategoryListAsync
        public async Task<List<IdNameViewModel>> GetCategoryListAsync()
        {
            var list = await _categoryRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }
        #endregion

        #region GetColorListAsync
        public async Task<List<IdNameViewModel>> GetColorListAsync()
        {
            var list = await _colorsRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }
        #endregion

        #region GetFilterListAsync
        public async Task<List<IdNameViewModel>> GetFilterListAsync()
        {
            var list = await _filterRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }
        #endregion

        #region GetFilterListForProductEditAsync
        public async Task<List<IdNameViewModel>> GetFilterListForProductEditAsync(int categorId)
        {
            var list = await _filterRepository.GetAllAsync();
            var responseList = list.Where(x => x.CategoryId == categorId).Select(x => new IdNameViewModel { Id = x.Id, Name = x.Name }).ToList();
            return responseList;
        }
        #endregion

        #region GetWrapperForIndexView
        public async Task<ProductWrapperViewModel> GetWrapperForIndexView(ProductFilter filter)
        {
            ProductWrapperViewModel ResponseModel = new ProductWrapperViewModel
            {
                TotalCount = _productRepository.GetIndexViewTotalCount(filter)
            };
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            List<Product> list = await _productRepository.GetIndexViewRecordsAsync(filter, (filter.PageIndex - 1) * filter.PageSize, filter.PageSize);
            ResponseModel.ProductList = list.Select((x, index) => new ProductListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                CategoryName = x.Category.Name,
                PublishStatus = x.IsPublish ? "Published" : "Unpublished",
                Number = ResponseModel.PagingData.FromRecord + index,
                FilterName = x.Subcategory?.Name
            }).ToList();
            ResponseModel.PagingData = new PagingData(ResponseModel.TotalCount, filter.PageSize, filter.PageIndex);
            return ResponseModel;
        }
        #endregion

        #region DeleteProduct
        public async Task<int> DeleteProduct(int id)
        {
            var record = await _productRepository.FindByIdAsync(id);
            if (record != null)
                record.IsDeleted = true;
            var result = await _unitOfWork.SaveChangesAsync();
            return result;
        }
        #endregion

        #region GetForDetailsAsync
        public async Task<ProductDetailsViewModel> GetForDetailsAsync(int id)
        {
            ProductDetailsViewModel response;
            Product record = await _productRepository.FindByIdAsync(id, true);
            if (record != null)
            {
                response = new ProductDetailsViewModel()
                {
                    Id = record.Id,
                    Accessories = record.IncludedAccessories,
                    CreatedDate = record.CreatedDateTime.ToStringDateTimePattern(),
                    ModifiedDate = record.ModifiedDateTime.ToStringDateTimePattern(),
                    CategoryName = record.Category.Name,
                    DeliveryDays = record.DeliveryDays,
                    FilterName = record.Subcategory?.Name,
                    IsAvailable = record.IsAutomateStockMaintainance ? "Yes" : "No",
                    IsExclusive = record.IsExclusive ? "Yes" : "No",
                    LongDescription = record.Description,
                    MaterialType = record.MaterialType,
                    PrecautionsInstructions = record.Precautions,
                    ProductHeight = record.Height,
                    ProductLength = record.Length,
                    ProductName = record.Name,
                    Brand = record.Brand,
                    Quantity = record.Quantity,
                    UnitName = record.Unit?.UnitName,
                    StockKeepingUnit = record.StockKeepingUnit,
                    Status = record.IsPublish ? "Published" : "Not Published",
                    ProductWeight = record.Weight,
                    ProductWidth = record.Width,
                    AvailableQuantity = record.AvailableQuantity,
                };
                if (record.OriginalPrice != null)
                    response.OriginalPrice = string.Format("{0:0.00}", record.OriginalPrice);
                response.DiscountedPrice = string.Format("{0:0.00}", record.DiscountedPrice);
                foreach (var image in record.ProductImages)
                {
                    if (image.IsMain)
                        response.MainImageUrl = image.Image;
                    else
                        response.ImagesPaths.Add(image.Image);
                }

                if (record.ProductColors.Count > 0)
                {
                    var colorsArray = record.ProductColors.Select(x => x.Colors.Name).ToArray();
                    response.AvailableColors = string.Join(", ", colorsArray);
                }

                if (record.ProductSizes.Count > 0)
                {
                    var sizeArray = record.ProductSizes.Select(x => x.Size.ProductSize).ToArray();
                    response.AvailableSizes = string.Join(", ", sizeArray);
                }


                return response;
            }
            return null;
        }
        #endregion

        #region Edit Product Get Method
        public async Task<AddProductViewModel> GetForEditAsync(int id)
        {
            AddProductViewModel response;
            Product record = await _productRepository.FindByIdAsync(id, true);
            if (record != null)
            {
                response = new AddProductViewModel()
                {
                    Id = record.Id,
                    Accessories = record.IncludedAccessories,
                    CategoryId = record.CategoryId,
                    DeliveryDays = record.DeliveryDays,
                    FilterId = record.SubcategoryId,
                    IsAvailable = record.IsAutomateStockMaintainance,
                    IsExclusive = record.IsExclusive,
                    LongDescription = record.Description,
                    MaterialType = record.MaterialType,
                    PrecautionsInstructions = record.Precautions,
                    ProductHeight = record.Height,
                    ProductLength = record.Length,
                    ProductName = record.Name,
                    StockKeepingUnit = record.StockKeepingUnit,
                    Status = record.IsPublish ? "Publish" : "Unpublish",
                    ProductWeight = record.Weight,
                    ProductWidth = record.Width,
                    Brand = record.Brand,
                    Quantity = record.Quantity,
                    AvailableQuantity = record.AvailableQuantity,
                    UnitId = record.UnitId ?? 0,
                };
                if (record.OriginalPrice != null)
                    response.OriginalPrice = decimal.Round(record.OriginalPrice.Value);
                response.DiscountedPrice = decimal.Round(record.DiscountedPrice);
                response.MainImageText = record.ProductImages.FirstOrDefault(x => x.IsMain == true)?.Image;
                response.SelectedColorIds = record.ProductColors.Select(x => x.ColorsId).ToList();
                response.SelectedSizeIds = record.ProductSizes.Select(x => x.SizeId).ToList();

                foreach (var img in record.ProductImages)
                {
                    if (img.IsMain != true)
                    {
                        IdNameViewModel model = new IdNameViewModel()
                        {
                            Id = img.Id,
                            Name = img.Image
                        };
                        response.EditViewImageList.Add(model);
                    }
                    else
                    {
                        response.MainImageText = img.Image;
                        response.MainImageId = img.Id;
                    }
                }

                return response;
            }
            return null;
        }
        #endregion

        #region Edit Product Post Method
        public async Task<int> EditProductSaveAsync(int id, AddProductViewModel model)
        {
            Product databaseModel = await _productRepository.FindByIdAsync(id);
            if (databaseModel != null)
            {
                databaseModel.AvailableQuantity = model.AvailableQuantity.Value;
                if (databaseModel.IsAutomateStockMaintainance == true)
                {
                    if (model.IsAvailable == false)
                    {
                        databaseModel.AvailableQuantity = 0;
                    }
                }
                databaseModel.CategoryId = model.CategoryId;
                databaseModel.DeliveryDays = model.DeliveryDays;
                databaseModel.Description = model.LongDescription;
                databaseModel.DiscountedPrice = model.DiscountedPrice;
                databaseModel.SubcategoryId = model.FilterId;
                databaseModel.IncludedAccessories = model.Accessories;
                databaseModel.IsAutomateStockMaintainance = model.IsAvailable;
                databaseModel.MaterialType = model.MaterialType;
                databaseModel.IsExclusive = model.IsExclusive;
                databaseModel.IsPublish = model.Status.ToLower() == "publish";
                databaseModel.Name = model.ProductName;
                databaseModel.OriginalPrice = model.OriginalPrice;
                databaseModel.Precautions = model.PrecautionsInstructions;
                databaseModel.StockKeepingUnit = model.StockKeepingUnit;
                databaseModel.Weight = model.ProductWeight;
                databaseModel.Width = model.ProductWidth;
                databaseModel.Length = model.ProductLength;
                databaseModel.Height = model.ProductHeight;
                databaseModel.Brand = model.Brand;
                databaseModel.Quantity = model.Quantity;
                databaseModel.UnitId = model.UnitId;
                databaseModel.DiscountPercentage = 0;                    // here we have to calculate percentage

                if (model.IsAvailable == true)
                {
                    databaseModel.AvailableQuantity = model.AvailableQuantity.Value;
                }
            }
            var result = await _unitOfWork.SaveChangesAsync();

            await RemoveDeletedImages(model.DeleteIds);

            if (model.Image != null)
                await UpdateMainImage(model.MainImageText, databaseModel.Id, true);
            if (model.Image1 != null)
                await SaveImages(model.Image1RelativePath, databaseModel.Id, false);
            if (model.Image2 != null)
                await SaveImages(model.Image2RelativePath, databaseModel.Id, false);
            if (model.Image3 != null)
                await SaveImages(model.Image3RelativePath, databaseModel.Id, false);
            if (model.Image4 != null)
                await SaveImages(model.Image4RelativePath, databaseModel.Id, false);
            // save colors

            await RemovePreviousSavedColor(id);
            await SaveColors(model.SelectedColorIds, databaseModel.Id);
            //save sizes
            await RemovePreviousSavedSize(id);
            await SaveSizes(model.SelectedSizeIds, databaseModel.Id);
            return result;
        }
        #endregion

        #region PrivateMethods
        private async Task RemovePreviousSavedSize(int productId)
        {
            var sizes = await _productSizeRepository.GetAllAsync(productId);
            foreach (var item in sizes)
            {
                _productSizeRepository.Remove(item);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task RemovePreviousSavedColor(int productId)
        {
            var colors = await _productColorRepository.GetAllAsync(productId);
            foreach (var item in colors)
            {
                _productColorRepository.Remove(item);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        private async Task RemoveDeletedImages(string deleteIds)
        {
            if (deleteIds != null)
            {
                var deleteIdsArray = deleteIds.Split(',');
                foreach (var id in deleteIdsArray)
                {
                    if (!string.IsNullOrEmpty(id))
                    {
                        var record = _productImagesRepository.FindById(int.Parse(id));
                        if (record != null)
                        {
                            _productImagesRepository.Remove(record);
                            await _unitOfWork.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        #endregion

        #region GetUnitListAsync
        public async Task<List<IdNameViewModel>> GetUnitListAsync()
        {
            var list = await _unitRepository.GetAllAsync();
            var responseList = list.Select(x => new IdNameViewModel { Id = x.Id, Name = x.UnitName }).ToList();
            return responseList;
        }
        #endregion

        #region Private Methods
       
        #endregion
    }
}
