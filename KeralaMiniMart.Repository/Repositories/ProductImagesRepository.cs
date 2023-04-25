using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;
using System.Linq;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ProductImagesRepository : Repository<ProductImages>, IProductImagesRepository
    {
        public ProductImagesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public ProductImages FindMainImage(int productId)
        {
            return Set.Where(x => x.ProductId == productId && x.IsMain == true).FirstOrDefault();
        }
    }
}
