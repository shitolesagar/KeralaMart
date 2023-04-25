using KeralaMiniMart.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IProductSizeRepository : IRepository<ProductSize>
    {
        Task<List<ProductSize>> GetAllAsync(int productId);
    }
}
