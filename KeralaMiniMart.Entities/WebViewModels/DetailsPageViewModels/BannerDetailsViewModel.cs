using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class BannerDetailsViewModel
    {
        public string Caption { get; set; }
        public string ImageUrl { get; set; }

        public string ExpireDate { get; set; }
        public string StartDate { get; set; }

        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public int Id { get; set; }
    }
}
