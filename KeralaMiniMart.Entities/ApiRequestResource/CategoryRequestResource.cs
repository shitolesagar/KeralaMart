using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiRequestResource
{
    public class BannerResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }

    }

    public class BannerResourceWrapper
    {
        public List<BannerResource> BannerList { get; set; }
    }


    public class CategoryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

    }

    public class CategoryResourceWrapper
    {
        public List<CategoryResource> CategoryList { get; set; }
    }


    public class NotificationResource
    {
        public int Id { get; set; }
        public string NotificationType { get; set; }
        public string TimeStamp { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string ModifiedTitle { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }

    public class NotificationResourceWrapper
    {
        public List<NotificationResource> NotificationList { get; set; }
    }



    public class FilterResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class FilterResourceWrapper
    {
        public List<FilterResource> FilterList { get; set; }
    }



    public class ProductListRequestResource
    {
        public string AppId { get; set; }
        public int take { get; set; }
        public int CategoryId { get; set; }
        public ICollection<int> filters { get; set; }
        public bool ShowOnlyExclusive { get; set; }
    }
    public class ProductResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? FilterId { get; set; }
        public string Image { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public string CategoryName { get; set; }
        public bool IsExclusive { get; set; }
        public bool IsAvailable { get; set; }
        public int AvailableQuantity { get; set; }
        public ICollection<string> Filters { get; set; }
    }
    public class ProductResourceWrapper
    {
        public List<ProductResource> ProductList { get; set; }
    }



    public class ProductDetailsResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public int DiscountPercent { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string MaterialType { get; set; }
        public string IncludedAccesories { get; set; }
        public string Precautions { get; set; }
        public bool IsAvailable { get; set; }
        public string DeliveryTime { get; set; }
        public ICollection<ProductImagesResource> Images { get; set; }
        public ICollection<ProductColorsResource> ColorList { get; set; }
        public ICollection<ProductSizeResource> SizeList { get; set; }
        public string Brand { get; set; }
        public string Quantity { get; set; }
        public string UnitName { get; set; }
    }
    public class ProductDetailsResourceWrapper
    {
        public List<ProductResource> ProductList { get; set; }
        public List<string> FiltersList { get; set; }
        public int TotalCount { get; set; }
    }

    public class ProductImagesResource
    {
        public string Image { get; set; }
    }
    public class ProductColorsResource
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string HashCode { get; set; }
    }
    public class ProductSizeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApplyFiltersResource
    {
        public int CategoryId { get; set; }
        public string AppId { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public List<int> Filters { get; set; }
    }
}
