using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
