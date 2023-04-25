using KeralaMiniMart.Entities.ApiRequestResource;
using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.ApiResponseResource
{
    public class AppThemeResponse
    {
        public string PrimaryColor { get; set; }
        public string SecondryColor { get; set; }
        public string StatusBarColor { get; set; }
        public string TertiaryColor { get; set; }
        public string TextColor { get; set; }
        public string CurrencySymbol { get; set; }
        public string AppName { get; set; }
        public string AppLogoURL { get; set; }
    }

    public class ApplicationThemeResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public AppThemeResponse data { get; set; }
    }

    public class VersionResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public VersionControl data { get; set; }
    }

    public class BannerResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public BannerResourceWrapper data { get; set; }
    }

    public class CategoryResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public CategoryResourceWrapper data { get; set; }
    }

    public class NotificationResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public NotificationResourceWrapper data { get; set; }
    }

    public class FilterResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public FilterResourceWrapper data { get; set; }
    }

    public class ProductResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public ProductResourceWrapper data { get; set; }
    }

    public class ProductListResponse
    {
        public bool error { get; set; }
        public string Message { get; set; }
        public ProductDetailsResourceWrapper data { get; set; }
    }

    public class ProductDetailsResponse
    {
        public bool error { get; set; } = false;
        public string Message { get; set; }
        public ProductDetailsResource data { get; set; }
    }
}
