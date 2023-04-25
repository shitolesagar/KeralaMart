using KeralaMiniMart.Entities.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IUserDeviceInfoRepository : IRepository<UserDeviceInfo>
    {
        UserDeviceInfo FindByToken(string Token, int? ApplicationUserId);

        List<UserDeviceInfo> FindByApplicationUserId(int ApplicationUserId);

        List<UserDeviceInfo> GetAllListFromAddress(List<int> addressIds);
    }
}
