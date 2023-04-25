using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class AddProductViewModel
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Main image is required.")]
        public string MainImageText { get; set; }

        public IFormFile Image1 { get; set; }
        public string Image1RelativePath { get; set; }

        public IFormFile Image2 { get; set; }
        public string Image2RelativePath { get; set; }

        public IFormFile Image3 { get; set; }
        public string Image3RelativePath { get; set; }

        public IFormFile Image4 { get; set; }
        public string Image4RelativePath { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string LongDescription { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        public int? FilterId { get; set; }

        public List<int> SelectedColorIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }

        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Original Price is not valid.")]
        public decimal? OriginalPrice { get; set; }

        [Required(ErrorMessage = "Offer price is required.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Offer Price is not valid.")]
        public decimal DiscountedPrice { get; set; }



        public string StockKeepingUnit { get; set; }

        public bool IsAvailable { get; set; }


        // fields for edit
        public List<IdNameViewModel> EditViewImageList { get; set; } = new List<IdNameViewModel>();
        public string DeleteIds { get; set; }
        public int MainImageId { get; set; }

        // For KMM
        public string Brand { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Value for {0} must be numeric")]
        public int? AvailableQuantity { get; set; }
        public int? NewlyArrivedQuantity { get; set; }
        public int? FaultyProductsQuantity { get; set; }

        [Required(ErrorMessage = "Unit is required.")]
        public int UnitId { get; set; }

        [Required(ErrorMessage = "Product weight is required.")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Quantity is not valid.")]
        public string Quantity { get; set; }

        //For Crazypetels
        public string MaterialType { get; set; }
        public List<int> SelectedSizeIds { get; set; } = new List<int>();

        public string ProductLength { get; set; }
        public string ProductWidth { get; set; }
        public string ProductHeight { get; set; }
        public string ProductWeight { get; set; }

        public string Accessories { get; set; }
        public string PrecautionsInstructions { get; set; }
        public int DeliveryDays { get; set; }

        public bool IsExclusive { get; set; }
    }
}
