using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeralaMiniMart.Repository.Repositories
{
    public class UserDeviceInfoRepository : Repository<UserDeviceInfo>, IUserDeviceInfoRepository
    {
        public UserDeviceInfoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public UserDeviceInfo FindByToken(string Token, int? ApplicationUserId)
        {
            //return Set.Where(x => x.DeviceId == Token).FirstOrDefault();
            if (ApplicationUserId == 0 || ApplicationUserId == null)
                return Set.Where(x => x.DeviceId == Token && x.ApplicationUserId == null).FirstOrDefault();
            return Set.Where(x => x.DeviceId == Token && x.ApplicationUserId == ApplicationUserId).FirstOrDefault();
        }

        public List<UserDeviceInfo> FindByApplicationUserId(int ApplicationUserId)
        {
            return Set.Where(x => x.ApplicationUserId == ApplicationUserId).ToList();
        }

        public List<UserDeviceInfo> GetAllListFromAddress(List<int> addressIds)
        {
            return Set.Where(x => x.ApplicationUser.UserAddresses.Any(a => a.IsDeleted == false && addressIds.Any(userId => a.DeliveryLocationId == userId))).ToList();
        }
    }
}
