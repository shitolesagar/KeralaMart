using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IProductColorRepository : IRepository<ProductColor>
    {
        Task<List<ProductColor>> GetAllAsync(int productId);
    }
}
