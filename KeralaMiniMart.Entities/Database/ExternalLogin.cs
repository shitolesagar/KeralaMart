using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class ExternalLogin
    {
        public int Id { get; set; }
        public int LoginProvider { get; set; }
        public int ProviderKey { get; set; }

    }
}
