using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class NotificationDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public string AddedDate { get; set; }
        public string Category { get; set; }
        public List<string> SelectedAreas { get; set; } = new List<string>();
    }
}
