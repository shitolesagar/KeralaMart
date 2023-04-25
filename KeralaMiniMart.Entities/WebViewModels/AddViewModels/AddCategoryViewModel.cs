using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage= "Please enter category name.")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage ="Please select image.")]
        public string ImageUrl { get; set; }

        //[Required(ErrorMessage = "Please enter description.")]
        public string Description { get; set; }

        public IFormFile File { get; set; }
    }
}
