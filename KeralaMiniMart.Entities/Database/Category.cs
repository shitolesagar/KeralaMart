using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string ShortDescription { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }

        public ICollection<Subcategory> Filters { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
