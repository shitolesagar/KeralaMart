using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class AddNotificationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }

        public string Message { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }

        public IFormFile File { get; set; }

        public List<int> SelectedDeliveryLocationIds { get; set; } = new List<int>();

    }
}
