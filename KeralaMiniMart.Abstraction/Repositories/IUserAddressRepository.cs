using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IUserAddressRepository : IRepository<UserAddress>
    {
        Task<List<UserAddress>> GetAllAddressForUser(string AppId, int ApplicationUserId);
    }
}
