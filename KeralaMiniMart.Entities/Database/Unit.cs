using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Unit
    {
        public int Id { get; set; }
        public string UnitName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
