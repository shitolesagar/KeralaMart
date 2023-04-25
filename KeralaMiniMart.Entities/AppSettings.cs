using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Entities
{
    public class AppSettings
    {
        public string WebBaseUrl { get; set; }
        public string WebApiBaseUrl { get; set; }
        public string FCMServerApiKey { get; set; }
        public string FCMSenderId { get; set; }
    }
}
