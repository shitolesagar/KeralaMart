using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;
using System.Linq;

namespace KeralaMiniMart.Repository.Repositories
{
    public class DeliveryLocationRepository : Repository<DeliveryLocation>, IDeliveryLocationRepository
    {
        public DeliveryLocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public DeliveryLocation findByLocalityAndPincode(string locality, string Pincode)
        {
            return Set.Where(x => x.Area == locality && x.Pincode == Pincode).FirstOrDefault();
        }

        public List<DeliveryLocation> GetSimilaryDeliveryDaysAtLocation(int id)
        {
            var SelectedLocation = Set.Where(x => x.Id == id).FirstOrDefault();
            var DeliveryLocationDays = Set.Where(x => x.Pincode == SelectedLocation.Pincode && x.Area == SelectedLocation.Area).ToList();
            return DeliveryLocationDays;
        }
    }
}
