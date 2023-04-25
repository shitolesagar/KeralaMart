using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Banner
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
