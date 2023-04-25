using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublish { get; set; }
        public decimal? OriginalPrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public string Description { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsExclusive { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public bool IsAutomateStockMaintainance { get; set; } = false;
        public string StockKeepingUnit { get; set; }
        public bool IsDeleted { get; set; }
        public string Width { get; set; }
        public string Length { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string MaterialType { get; set; }
        public string IncludedAccessories { get; set; }
        public string Precautions { get; set; }
        public int DeliveryDays { get; set; }
        public int AvailableQuantity { get; set; }

        public string Brand { get; set; }
        public string Quantity { get; set; }

        public int? UnitId { get; set; }
        public Unit Unit { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public int? SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; }
        public ICollection<ProductSize> ProductSizes { get; set; }
    }
}
