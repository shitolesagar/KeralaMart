using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class ColorsRepository : Repository<Colors>, IColorsRepository
    {
        public ColorsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
