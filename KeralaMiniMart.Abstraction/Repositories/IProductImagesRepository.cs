using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Abstraction.Repositories
{
    public interface IProductImagesRepository : IRepository<ProductImages>
    {
        ProductImages FindMainImage(int productId);
    }
}
