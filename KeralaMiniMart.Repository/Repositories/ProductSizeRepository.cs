using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ProductSizeRepository : Repository<ProductSize>, IProductSizeRepository
    {
        public ProductSizeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<ProductSize>> GetAllAsync(int productId)
        {
            return Set.Where(x => x.ProductId == productId).ToListAsync();
        }
    }
}
