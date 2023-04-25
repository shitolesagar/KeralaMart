using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class AddSubcategoryViewModel
    {
        [Required(ErrorMessage= "Please select category.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Please enter subcategory name.")]
        public string SubcategoryName { get; set; }

        public string ImageUrl { get; set; }

        public IFormFile File { get; set; }

    }
}
