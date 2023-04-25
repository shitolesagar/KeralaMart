using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities.WebViewModels
{
    public class NotificationWrapperViewModel
    {
        public List<NotificationListViewModel> NotificationList { get; set; }
        public PagingData PagingData { get; set; }
        public int TotalCount { get; set; }
    }

    public class NotificationListViewModel
    {
        public int Number { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string SentDate { get; set; }
        public string NotificationType { get; set; }
    }
}
