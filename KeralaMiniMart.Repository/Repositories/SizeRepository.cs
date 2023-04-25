using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Entities.Database;

namespace KeralaMiniMart.Repository.Repositories
{
    public class SizeRepository : Repository<Size>, ISizeRepository
    {
        public SizeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
