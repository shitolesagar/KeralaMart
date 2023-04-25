using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeralaMiniMart.Entities.Database
{
    public class VersionControl
    {
        public int Id { get; set; }
        public float VersionNumber { get; set; }
        public DateTime Date { get; set; }
        public bool CurrentLiveVersion { get; set; }
        public string UpdateType { get; set; }
    }
}
