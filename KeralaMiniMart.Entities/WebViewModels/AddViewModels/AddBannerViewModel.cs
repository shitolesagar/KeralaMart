using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class AddBannerViewModel
    {
        [Required(ErrorMessage = "Please enter caption/title.")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Please select Image.")]
        public string ImageUrl { get; set; }

       // [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ExpireDate { get; set; }
        public DateTime? StartDate { get; set; }

        public IFormFile File { get; set; }
    }
}
