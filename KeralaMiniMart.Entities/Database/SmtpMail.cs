using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class SmtpMail
    {
        public int Id { get; set; }
        public string FromMail { get; set; }
        public string SmtpPassword { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}
