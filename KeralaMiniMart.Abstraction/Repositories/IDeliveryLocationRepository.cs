using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IDeliveryLocationRepository : IRepository<DeliveryLocation>
    {
        DeliveryLocation findByLocalityAndPincode(string locality, string Pincode);
        List<DeliveryLocation> GetSimilaryDeliveryDaysAtLocation(int id);
    }
}
